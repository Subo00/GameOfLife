using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _aliveColor;
    [SerializeField] private Image _tileImage;
    [SerializeField] private GameObject _hightlight;
    
    private GridHandler _grid;

    private Vector2 _id;
    public bool isAlive;
    public bool isAliveNextTurn = false;
    public int numOfNeighbours;

    public void Init(Vector2 id)
    {
        SetAlive(false);
        numOfNeighbours = 0;
        _id = id;
        isAliveNextTurn = false;
        _grid = this.transform.parent.GetComponent<GridHandler>();
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        UpdateTile();
    }
    public void NextTurn()
    {
        SetAlive(isAliveNextTurn);
        isAliveNextTurn = false;
    }
    public void UpdateTile(){ _tileImage.color = isAlive ? _aliveColor : _baseColor; }
    public void ActivateHighlight(bool active){_hightlight.SetActive(active);}
    public void DestroyTile(){   Destroy(gameObject); }

    public void OnPointerClick(PointerEventData eventData){ /*SetAlive(!isAlive);*/  }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ActivateHighlight(true);
        if(eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>();
            _grid.HoverElement(_id, card.GetDrawInstructions(), true);
            card.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ActivateHighlight(false);
        if(eventData.pointerDrag != null)
        {
            Card card = eventData.pointerDrag.GetComponent<Card>(); //get the card component
            _grid.HoverElement(_id, card.GetDrawInstructions(), false);//send instructions to parent
            card.GetComponent<CanvasGroup>().alpha = 1;
        } 
    }

    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        if(card != null)
        {
            _grid.DrawElement(_id, card.GetDrawInstructions());
            eventData.pointerDrag.GetComponent<Draggable>().DestroySelf();
        } 
    }

    

}
