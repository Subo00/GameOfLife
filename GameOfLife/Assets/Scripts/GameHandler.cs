using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]private Player _player0;
    [SerializeField]private float _delay;
    [SerializeField]private int _numOfIterations;
    [SerializeField]private GridHandler _grid;
    [SerializeField]private GameObject _buttons;
    private int _turns = 0;
    private int _maxTurns = 6;

   
    public void NextTurn()
    {
        StartCoroutine(Loop());   
    }
    IEnumerator Loop()
    {
        _buttons.SetActive(false);
        _player0.gameObject.SetActive(false);
        Debug.Log("Turn: " + _turns.ToString());
        for(int i =  0; i < _numOfIterations; i++)
        {
             _grid.Iterate();
            yield return new WaitForSeconds(_delay);
        }
        
        _buttons.SetActive(true);     
        _player0.gameObject.SetActive(true);
        _player0.DrawCard();
        _player0.ResetAction();
        _turns++;
        if(_turns == _maxTurns) Debug.Log("End Game");
    }
}
