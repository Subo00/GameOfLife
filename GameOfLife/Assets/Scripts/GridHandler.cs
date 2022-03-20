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

    public void DrawElement(int id)
    {
        _tiles[id].SetAlive(true);
        _tiles[id-_squareSide+1].SetAlive(true);
        _tiles[id-_squareSide+2].SetAlive(true);
        _tiles[id+_squareSide+1].SetAlive(true);
        _tiles[id+_squareSide+2].SetAlive(true);
        _tiles[id+3].SetAlive(true);
    }
}
