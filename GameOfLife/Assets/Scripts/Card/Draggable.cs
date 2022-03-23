using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler
{
    public Transform parentToReturnTo = null;
    private CanvasGroup _canvasGroup;
    private Card card;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        card = this.GetComponent<Card>();
    }
   
    public void OnBeginDrag(PointerEventData eventData)
    {        
        parentToReturnTo = this.transform.parent;
        //play sound
        this.transform.SetParent(this.transform.root);
        _canvasGroup.blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //call to parent check if this card can be played
        if(parentToReturnTo.GetComponent<Player>().CanPlay(card.GetCost()))
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //play sound
        this.transform.SetParent(parentToReturnTo);
        _canvasGroup.blocksRaycasts = true;
    }

    public void DestroySelf()
    { 
        Destroy(gameObject);
        parentToReturnTo.GetComponent<Player>().SpendAction(card.GetCost());
        
    }
}
    
