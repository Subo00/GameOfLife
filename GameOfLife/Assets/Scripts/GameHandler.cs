using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameHandler : MonoBehaviour
{
    [SerializeField]private Player _player0;
    [SerializeField]private float _delay;
    [SerializeField]private int _numOfIterations;
    [SerializeField]private GridHandler _grid;
    [SerializeField]private GameObject _buttons;
    [SerializeField] TMP_Text _turnsDisplay;
    [SerializeField] TMP_Text _globalDisplay;
    private int _turns = 0;
    private int _maxTurns = 6;
    private int _globalScore = 0;

    public void NextTurn(){  StartCoroutine(Loop()); }
    public void Draw(){ if(_player0.CanPlay(1)){ _player0.DrawCard(); _player0.SpendAction(1);} }

    
    IEnumerator Loop()
    {
        _buttons.SetActive(false);
        _player0.gameObject.SetActive(false);
        
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
        UpdateTurns();

        _globalScore += _grid.GetAliveCount();
        UpdateGlobal();

        if(_turns == _maxTurns) Debug.Log("End Game");
    }

    private void UpdateTurns(){ _turnsDisplay.text = _turns.ToString() + "/" + _maxTurns.ToString(); }
    private void UpdateGlobal(){ _globalDisplay.text = _globalScore.ToString(); }
}
