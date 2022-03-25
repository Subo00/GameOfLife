using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] private List<Card> _deck;
    [SerializeField] TMP_Text _deckDisplay;
    [SerializeField] TMP_Text _actionDisplay;
    private int _maxActions = 4;
    private int _actions;
    void Start()
    {
        ShuffleDeck();
        for(int i = 0; i < 3; i++) DrawCard();  
        ResetAction();
    }
    
    public bool CanPlay(int cost){ return _actions >= cost ? true : false; }
    public void SpendAction(int cost){_actions -= cost; UpdateAction();}
    private void UpdateAction(){ _actionDisplay.text = _actions.ToString() + "/" + _maxActions;}
    private void UpdateDeck(){ _deckDisplay.text = _deck.Count.ToString();}
    public void ResetAction(){ _actions = _maxActions; UpdateAction();}
    
    public void DrawCard()
    {
        
        if(_deck != null)
        {
            Instantiate(_deck[0], this.transform);
            _deck.RemoveAt(0);
            UpdateDeck();
        }
        else
        {
            Debug.LogWarning("Deck is Empty!");
        }
    }

    
    public void ShuffleDeck()
    {
        int n = _deck.Count;
        while(n > 0)
        {
            n--;
            int k = Random.Range(0, n);
            Card tmp = _deck[k];
            _deck[k] = _deck[n];
            _deck[n] = tmp;
        }
    }

}
