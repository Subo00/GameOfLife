using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public float rotationSpeed = 100f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float rotateX = 0f;
        float rotateY = 0f;

        if (Input.GetKey(KeyCode.W))
            rotateX = 1f;
        if(Input.GetKey(KeyCode.S))
            rotateX = -1f;

        if (Input.GetKey(KeyCode.A))
            rotateY = 1f;
        if (Input.GetKey(KeyCode.D))
            rotateY = -1f;

        transform.Rotate(rotateX * rotationSpeed * Time.deltaTime, rotateY * rotationSpeed * Time.deltaTime, 0f);

    }
}
