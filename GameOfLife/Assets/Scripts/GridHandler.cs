using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridHandler : MonoBehaviour
{
    [SerializeField] private int _squareSide;
    [SerializeField] private Tile _tilePrefab;
    [Range(0.1f, 0.7f)]
    [SerializeField] private float _randomValue;
    private RectTransform _rectTransform;
    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _tiles = new Dictionary<Vector2, Tile>();
        CreateGrid();
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

    public void ClearGrid(){ foreach(KeyValuePair<Vector2, Tile> entry in _tiles) { entry.Value.SetAlive(false); entry.Value.numOfNeighbours = 0; } }

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

       foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
       {
           var tile = entry.Value;
           if(tile.isAlive && (tile.numOfNeighbours == 2 || tile.numOfNeighbours == 3))
           {
                tile.isAliveNextTurn = true;
           }
           else if(!tile.isAlive && tile.numOfNeighbours == 3)
           {
                tile.isAliveNextTurn = true;    
           }
       } 

        ClearGrid();
        foreach(KeyValuePair<Vector2, Tile> entry in _tiles)
        {
            entry.Value.NextTurn(); //uses the isAliceNextTurn flag
        }
    }
  
    #region HoverElements

    public void HoverElement(Vector2 id, string elementName, bool set)
    {
        switch(elementName)
        {
            case "Beehive":
                BeehiveHover(id, set); break;
            case "Block": 
                BlockHover(id, set); break;
            case "Glider":
                GliderHover(id, set); break;
            case "Beacon":
                BeaconHover(id, set); break;
        }
    }
    private void BeehiveHover(Vector2 id, bool set)
    {
        Tile tile;
        GetTileAtPosition(id).ActivateHighlight(set); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+1));
        if(tile != null) tile.ActivateHighlight(set);             //      
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+2));    //      ##  
        if(tile != null) tile.ActivateHighlight(set);             // id->#  #
        tile = GetTileAtPosition(new Vector2(id.x, id.y+3));      //      ##      
        if(tile != null) tile.ActivateHighlight(set);             //
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+1));
        if(tile != null) tile.ActivateHighlight(set);
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+2));    
        if(tile != null) tile.ActivateHighlight(set);
    }
    
    private void BlockHover(Vector2 id,bool set)
    {
        Tile tile;
        GetTileAtPosition(id).ActivateHighlight(set); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y));
        if(tile != null) tile.ActivateHighlight(set);           // id->##
        tile = GetTileAtPosition(new Vector2(id.x, id.y+1));    //     ##
        if(tile != null) tile.ActivateHighlight(set);                     
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+1));
        if(tile != null) tile.ActivateHighlight(set);
    }
    private void GliderHover(Vector2 id, bool set)
    {
        Tile tile;
        GetTileAtPosition(id).ActivateHighlight(set); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x, id.y-1));
        if(tile != null) tile.ActivateHighlight(set);
        tile = GetTileAtPosition(new Vector2(id.x, id.y+1));      //    #
        if(tile != null) tile.ActivateHighlight(set);             //     #   
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+1));    //   ###
        if(tile != null) tile.ActivateHighlight(set);             //  id^
        tile = GetTileAtPosition(new Vector2(id.x-2, id.y));
        if(tile != null) tile.ActivateHighlight(set);
    }

    private void BeaconHover(Vector2 id, bool set)
    {
        Tile tile;
        tile = GetTileAtPosition(new Vector2(id.x, id.y-1));
        if(tile != null) tile.ActivateHighlight(set);
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y-1));
        if(tile != null) tile.ActivateHighlight(set);
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y));       // ##
        if(tile != null) tile.ActivateHighlight(set);              // # <-id
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+2));     //    #
        if(tile != null) tile.ActivateHighlight(set);              //   ##
        tile = GetTileAtPosition(new Vector2(id.x+2, id.y+1));     //
        if(tile != null) tile.ActivateHighlight(set);
        tile = GetTileAtPosition(new Vector2(id.x+2, id.y+2));
        if(tile != null) tile.ActivateHighlight(set);
    }
    #endregion


    
    #region drawElements
    public void DrawElement(Vector2 id, string elementName)
    {
        switch(elementName)
        {
            case "Beehive":
                Beehive(id); break;
            case "Block": 
                Block(id); break;
            case "Glider":
                Glider(id); break;
            case "Beacon":
                Beacon(id); break;
        }

        HoverElement( id, elementName, false);
    }

    private void Beehive(Vector2 id)
    {
        Tile tile;
        GetTileAtPosition(id).SetAlive(true); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+1));
        if(tile != null) tile.SetAlive(true);                     //      
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+2));    //      ##  
        if(tile != null) tile.SetAlive(true);                     // id->#  #
        tile = GetTileAtPosition(new Vector2(id.x, id.y+3));      //      ##      
        if(tile != null) tile.SetAlive(true);                     //
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+1));
        if(tile != null) tile.SetAlive(true);
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+2));    
        if(tile != null) tile.SetAlive(true);
    }

    
    private void Block(Vector2 id)
    {
        Tile tile;
        GetTileAtPosition(id).SetAlive(true); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y));
        if(tile != null) tile.SetAlive(true);                   // id->##
        tile = GetTileAtPosition(new Vector2(id.x, id.y+1));    //     ##
        if(tile != null) tile.SetAlive(true);                     
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+1));
        if(tile != null) tile.SetAlive(true);
    }

    private void Glider(Vector2 id)
    {
        Tile tile;
        GetTileAtPosition(id).SetAlive(true); //I dont' need to check this one, this one is garanteed
        tile = GetTileAtPosition(new Vector2(id.x, id.y-1));
        if(tile != null) tile.SetAlive(true);
        tile = GetTileAtPosition(new Vector2(id.x, id.y+1));    //    #
        if(tile != null) tile.SetAlive(true);                   //     #   
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y+1));  //   ###
        if(tile != null) tile.SetAlive(true);                   //  id^
        tile = GetTileAtPosition(new Vector2(id.x-2, id.y));
        if(tile != null) tile.SetAlive(true);
    }

    private void Beacon(Vector2 id)
    {
        Tile tile;
        tile = GetTileAtPosition(new Vector2(id.x, id.y-1));
        if(tile != null) tile.SetAlive(true);        
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y-1));
        if(tile != null) tile.SetAlive(true);        
        tile = GetTileAtPosition(new Vector2(id.x-1, id.y));     // ##
        if(tile != null) tile.SetAlive(true);                      // # <-id
        tile = GetTileAtPosition(new Vector2(id.x+1, id.y+2));     //    #
        if(tile != null) tile.SetAlive(true);                      //   ##
        tile = GetTileAtPosition(new Vector2(id.x+2, id.y+1));     //
        if(tile != null) tile.SetAlive(true);        
        tile = GetTileAtPosition(new Vector2(id.x+2, id.y+2));
        if(tile != null) tile.SetAlive(true);        
    }
    #endregion
}
