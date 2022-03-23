using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridHandler : MonoBehaviour
{
    [SerializeField] private int _squareSide;
    [SerializeField] private Tile _tilePrefab;
    [Range(0.1f, 0.7f)]
    [SerializeField] private float _randomValue;
    [SerializeField] private TMP_Text _scoreDisplay;
    private RectTransform _rectTransform;
    private Dictionary<Vector2, Tile> _tiles;
    private int _aliveCount;


    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _tiles = new Dictionary<Vector2, Tile>();
        _aliveCount = 0;
        CreateGrid();
        FillGrid();
    }

    public void CreateGrid()
    {
        ResizeCellSize();        
        for(int x = 0; x < _squareSide; x++)
        {
            for(int y = 0; y < _squareSide; y++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, this.transform);
                Vector2 pos = new Vector2(x,y);
                spawnedTile.Init(pos);
                _tiles[pos] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile)){ return tile;  }
        return null;
    }

    private void ResizeCellSize() //resizes the tiles that will be under the grid
    {
        float maxSize = _rectTransform.rect.width; 
        maxSize /= _squareSide;
        Vector2 reSize = new Vector2(maxSize, maxSize);
        this.GetComponent<GridLayoutGroup>().cellSize = reSize;
    }

    public void ClearGrid(){ foreach(KeyValuePair<Vector2, Tile> entry in _tiles){ entry.Value.SetAlive(false); entry.Value.numOfNeighbours = 0;} }

    public void FillGrid()
    {
        ClearGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            bool set = Random.Range(0f, 1f) < _randomValue ? true : false;
            entry.Value.SetAlive(set);
        }
    }

     public void Iterate()
    {
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
        _aliveCount = 0;
       foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
       {
           var tile = entry.Value;
           if(tile.isAlive && (tile.numOfNeighbours == 2 || tile.numOfNeighbours == 3))
           {
                tile.isAliveNextTurn = true; _aliveCount++;
           }
           else if(!tile.isAlive && tile.numOfNeighbours == 3)
           {
                tile.isAliveNextTurn = true; _aliveCount++;
           }
       } 

        ClearGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            entry.Value.NextTurn(); //uses the isAliveNextTurn flag
        }
        ShowAliveCount();
    }
  
    #region CardInteraction

    public void HoverElement(Vector2 id,List<Vector2> drawInstructions, bool set)
    {
        Tile tile;
        foreach(var offset in drawInstructions)
        {
            tile = GetTileAtPosition(id + offset);
            if(tile != null) tile.ActivateHighlight(set);
        }
    }
 
    public void DrawElement(Vector2 id, List<Vector2> drawInstructions)
    {
        Tile tile;
        foreach(var offset in drawInstructions)
        {
            tile = GetTileAtPosition(id + offset);
            if(tile != null) tile.SetAlive(true);
        }

        HoverElement( id, drawInstructions, false);
    }

    #endregion
    
    public void ShowAliveCount(){_scoreDisplay.text = _aliveCount.ToString();}
    /*
    public void ChangeColumns(float columns){ _columns = (int)columns; }
    public void ChangeRows(float rows){ _rows = (int)rows; }
    public void ChangeRand(float randomValue){ _randomValue = randomValue;}
    */
}
