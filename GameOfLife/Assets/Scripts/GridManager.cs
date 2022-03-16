using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private int _maxWidth, _maxHeight;
    [SerializeField] private Tile  _tilePrefab;
    [SerializeField] private Transform _cam;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float _randValue;

    private float _scaleX = 1f;
    private float _scaleY = 1f;

    private Dictionary<Vector2, Tile> _tiles;
    void Start()
    {
        GenerateGrid();
        FillGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        _scaleX = _width > _maxWidth ? (float)_maxWidth/(float)_width : 1f;
        _scaleY = _height > _maxHeight ? (float)_maxHeight/(float)_height : 1f;
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                

                _tiles[new Vector2(x,y)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float)_width/2 - 0.5f,
                                              (float)_height/2 - 0.5f,
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

    public void ClearGrid()
    {
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            entry.Value.isAlive = false;
            entry.Value.UpdateTile();
        }         
    }

    public void FillGrid()
    {
        ClearGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            bool set = Random.Range(0f, 1f) < _randValue ? true : false;
            entry.Value.isAlive = set;
            entry.Value.UpdateTile();
        }
    }
}
