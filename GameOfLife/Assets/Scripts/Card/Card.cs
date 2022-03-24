using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    [SerializeField] private List<Vector2> _drawInstructions;
    [SerializeField] private int _cost;


    public int GetCost(){ return _cost; }
    public List<Vector2> GetDrawInstructions(){ return _drawInstructions;}
}
