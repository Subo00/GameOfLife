using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    [SerializeField] private Color _aliveColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _hightlight;

    
    private bool _isOffset;

    public bool isAlive;

    public void Init(bool isOffset)
    {
        isAlive = false;
        _isOffset = isOffset;
        UpdateTile();
    }

    void OnMouseEnter()
    {
        _hightlight.SetActive(true);
    }

    void OnMouseExit() 
    {
        _hightlight.SetActive(false);
    }

    
    void OnMouseDown() 
    {
        isAlive = !isAlive;
        UpdateTile();
    }

    public void UpdateTile()
    {
        _renderer.color = isAlive ? _aliveColor : (_isOffset ? _offsetColor : _baseColor);
    }
}
