using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridHandler : MonoBehaviour
{
    [SerializeField] private int _squareSide;
    [SerializeField] private Tile _tilePrefab;
    private RectTransform _rectTransform;
    private List<Tile> _tiles;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _tiles = new List<Tile>();
        CreateGrid();
    }

    public void CreateGrid()
    {
        ResizeCellSize();

        for(int i = 0; i < _squareSide; i++)
        {
            for(int j = 0; j < _squareSide; j++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, this.transform);
                spawnedTile.Init(i * _squareSide + j);
                _tiles.Add(spawnedTile);
            }
        }
    }

    private void ResizeCellSize() //resizes the tiles that will be under the grid
    {
        float maxSize = _rectTransform.rect.width; 
        maxSize /= _squareSide;
        Vector2 reSize = new Vector2(maxSize, maxSize);
        this.GetComponent<GridLayoutGroup>().cellSize = reSize;
    }



    #region HoverElements

    public void HoverElement(int id, string elementName, bool set)
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
    private void BeehiveHover(int id, bool set)
    {
        _tiles[id].ActivateHighlight(set);
        _tiles[id-_squareSide+1].ActivateHighlight(set);
        _tiles[id-_squareSide+2].ActivateHighlight(set);
        _tiles[id+_squareSide+1].ActivateHighlight(set);
        _tiles[id+_squareSide+2].ActivateHighlight(set);
        _tiles[id+3].ActivateHighlight(set);
    }

    private void BlockHover(int id,bool set)
    {
        _tiles[id].ActivateHighlight(set);
        _tiles[id+1].ActivateHighlight(set);
        _tiles[id+_squareSide+1].ActivateHighlight(set);
        _tiles[id+_squareSide].ActivateHighlight(set);
    }
    private void GliderHover(int id, bool set)
    {
        _tiles[id].ActivateHighlight(set);
        _tiles[id+1].ActivateHighlight(set);
        _tiles[id+2].ActivateHighlight(set);
        _tiles[id-_squareSide+2].ActivateHighlight(set);
        _tiles[id-_squareSide*2 + 1].ActivateHighlight(set);
    }

    private void BeaconHover(int id, bool set)
    {
        _tiles[id].ActivateHighlight(set);
        _tiles[id+1].ActivateHighlight(set);
        _tiles[id+_squareSide].ActivateHighlight(set);
        _tiles[id+_squareSide*2+3].ActivateHighlight(set);
        _tiles[id+_squareSide*3+2].ActivateHighlight(set);
        _tiles[id+_squareSide*3+3].ActivateHighlight(set);
    }
    #endregion



    #region drawElements
    public void DrawElement(int id, string elementName)
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

    private void Beehive(int id)
    {
        _tiles[id].SetAlive(true);
        _tiles[id-_squareSide+1].SetAlive(true);
        _tiles[id-_squareSide+2].SetAlive(true);
        _tiles[id+_squareSide+1].SetAlive(true);
        _tiles[id+_squareSide+2].SetAlive(true);
        _tiles[id+3].SetAlive(true);
    }


    private void Block(int id)
    {
        _tiles[id].SetAlive(true);
        _tiles[id+1].SetAlive(true);
        _tiles[id+_squareSide+1].SetAlive(true);
        _tiles[id+_squareSide].SetAlive(true);
    }

    private void Glider(int id)
    {
        _tiles[id].SetAlive(true);
        _tiles[id+1].SetAlive(true);
        _tiles[id+2].SetAlive(true);
        _tiles[id-_squareSide+2].SetAlive(true);
        _tiles[id-_squareSide*2 + 1].SetAlive(true);
    }

    private void Beacon(int id)
    {
        _tiles[id].SetAlive(true);
        _tiles[id+1].SetAlive(true);
        _tiles[id+_squareSide].SetAlive(true);
        _tiles[id+_squareSide*2+3].SetAlive(true);
        _tiles[id+_squareSide*3+2].SetAlive(true);
        _tiles[id+_squareSide*3+3].SetAlive(true);
    }
    #endregion
}
