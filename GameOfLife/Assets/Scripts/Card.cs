using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Vector3 _dragOffset, _startingPos;
    private Camera _cam;
    [SerializeField] private float _speed = 10;
    private GridManager _gridManager;
    private bool _isDragging;

    void Awake()
    { 
        _cam = Camera.main; 
        _gridManager = GameObject.FindObjectOfType<GridManager>();
        if(_gridManager == null) Debug.LogError("There isn't a grid manager in the scene");

        _startingPos = transform.position;
    }

    void OnMouseDown(){_dragOffset = transform.position - GetMousePos();}

    void OnMouseDrag(){transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + _dragOffset, _speed * Time.deltaTime); }

    Vector3 GetMousePos()
    {
        var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    void OnMouseUp()
    {
        var mousePos = GetMousePos();
        Vector2 tilePos = new Vector2(mousePos.x, mousePos.y); 
        var tile = _gridManager.GetTileAtPosition(tilePos);
        if(tile != null) Debug.Log("I did something");
        Debug.Log("I did nothing!");

        transform.position = _startingPos;
    }
}
