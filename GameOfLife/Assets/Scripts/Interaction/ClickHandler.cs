using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) != true)
            { return; }

        Vector3 mousePosition = Input.mousePosition;
        Ray ray  = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        bool somethingHit = Physics.Raycast(ray, out hit);
        if(somethingHit)
        {
            hit.transform.GetComponent<Cube>().ToggleAlive();
        }
    }
}
