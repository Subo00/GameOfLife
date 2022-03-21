using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _aliveColor;
    [SerializeField] private Image _tileImage;
    [SerializeField] private GameObject _hightlight;

    private int _id;
    public bool isAlive;
    public int numOfNeighbours;

    public void Init(int id)
    {
        SetAlive(false);
        numOfNeighbours = 0;
        _id = id;
    }

    
    public void SetAlive(bool alive)
    {
        isAlive = alive;
        UpdateTile();
    }
    
    public void UpdateTile(){ _tileImage.color = isAlive ? _aliveColor : _baseColor; }

    public void DestroyTile(){   Destroy(gameObject); }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ActivateHighlight(true);
        if(eventData.pointerDrag != null)
        {
            Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
            this.transform.parent.GetComponent<GridHandler>().HoverElement(_id, card.name, true);
            card.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActivateHighlight(false);
        if(eventData.pointerDrag != null)
        {
            Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
            this.transform.parent.GetComponent<GridHandler>().HoverElement(_id, card.name, false);       
            card.GetComponent<CanvasGroup>().alpha = 1;
        } 
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
        if(card != null)
        {
            this.transform.parent.GetComponent<GridHandler>().DrawElement(_id, card.name);
            card.GetComponent<CanvasGroup>().alpha = 1;
        } 
    }

    public void ActivateHighlight(bool active){_hightlight.SetActive(active);}

}
