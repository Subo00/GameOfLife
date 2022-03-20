using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _offsetColor;
    [SerializeField] private Color _aliveColor;
    [SerializeField] private Image _tileImage;
    [SerializeField] private GameObject _hightlight;

    
    private bool _isOffset;

    public bool isAlive;
    public int numOfNeighbours;

    public void Init(bool isOffset)
    {
        isAlive = false;
        numOfNeighbours = 0;
        _isOffset = isOffset;
        UpdateTile();
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        UpdateTile();
    }
    
    public void UpdateTile()
    {
        _tileImage.color = isAlive ? _aliveColor : (_isOffset ? _offsetColor : _baseColor);
    }

    public void DestroyTile()
    {
        Destroy(gameObject);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _hightlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hightlight.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData){}
}
