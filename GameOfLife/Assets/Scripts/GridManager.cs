using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private  int _rows, _columns;
    [SerializeField] private int _maxRows, _maxColumns;
    [SerializeField] private Tile  _tilePrefab;
    [SerializeField] private Transform _cam;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _randValue;

    private float _scaleX = 1f;
    private float _scaleY = 1f;

    private bool[,] _futureTiles;
    private int maxRange = 100;

    private Dictionary<Vector2, Tile> _tiles;
    void Start()
    {
        _futureTiles = new bool[maxRange, maxRange];
        ClearFutureGrid();
        CreateGrid();
        FillGrid();
    }

    public void CreateGrid()
    {
        //DestroyGrid();
        _tiles = new Dictionary<Vector2, Tile>();

        _scaleX = _rows > _maxRows ? (float)_maxRows/(float)_rows : 1f;
        _scaleY = _columns > _maxColumns ? (float)_maxColumns/(float)_columns : 1f;
        var scale = new Vector3(_scaleX, _scaleY);

        for(int x = 0; x < _rows; x++)
        {
            for(int y = 0; y < _columns; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x * _scaleX, y * _scaleY), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                spawnedTile.transform.localScale = scale;
                
                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_rows/2 * _scaleX - 0.5f,
                                              (float)_columns/2 * _scaleY - 0.5f,
                                               -10f);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    public void ClearGrid(){ foreach(KeyValuePair<Vector2, Tile> entry in _tiles) { entry.Value.SetAlive(false); entry.Value.numOfNeighbours = 0; } }

    public void FillGrid()
    {
        ClearGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            bool set = Random.Range(0f, 1f) < _randValue ? true : false;
            entry.Value.SetAlive(set);
        }
    }

    void DestroyGrid()
    { 
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles) 
        { 
            var tile = GetTileAtPosition(entry.Key);
            Destroy(tile);
        }
        _tiles.Clear();  
        _tiles = null;  
    }

    public void Iterate()
    {
        ClearFutureGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            if(entry.Value.isAlive)
            {   
                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        Vector2 temp = new Vector2((float)x, (float)y);
                        Vector2 newKey = entry.Key + temp;
                        var tile = GetTileAtPosition(newKey);
                        if(tile != null) tile.numOfNeighbours++;
                    }
                }
                entry.Value.numOfNeighbours--; //because we added himself as 
            }                                  //a neighbour in the loop
        } 

       foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
       {
           var tile = entry.Value;
           if(tile.isAlive && (tile.numOfNeighbours == 2 || tile.numOfNeighbours == 3))
           {
               _futureTiles[(int)entry.Key.x, (int)entry.Key.y] = true;
           }
           else if(!tile.isAlive && tile.numOfNeighbours == 3)
           {
               _futureTiles[(int)entry.Key.x, (int)entry.Key.y] = true;
           }
       } 

        ClearGrid();
        for(int x = 0; x < _rows; x++)
        {
            for(int y = 0; y < _columns; y++)
            {
                if(_futureTiles[x,y])
                {
                    Vector2 temp = new Vector2(x, y);
                    GetTileAtPosition(temp).SetAlive(true);
                }
            }
        }
    }

    void ClearFutureGrid()
    {
        for(int x = 0; x < _rows; x++)
        {
            for(int y = 0; y < _columns; y++)
            {
                _futureTiles[x,y] = false;
            }
        }
    }
    public void ChangeRows(float rows){ _rows = (int)rows; }
    public void ChangeColumns(float columns){ _columns = (int)columns; }
}
