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

    private bool _isAlive;
    private bool _isOffset;
    public void Init(bool isOffset)
    {
        _isAlive = false;
        _isOffset = isOffset;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
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
        _isAlive = !_isAlive;
        _renderer.color = _isAlive ? _aliveColor : (_isOffset ? _offsetColor : _baseColor);
    }
}
