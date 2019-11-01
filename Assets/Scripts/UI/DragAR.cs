using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAR : MonoBehaviour
{
    private Vector2 pressPos;
    private bool pressed = false;
    private float direction = 0;

    private float rotationSpeed = 0.5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!pressed)
            {
                pressed = true;
                pressPos = Input.mousePosition;
            }
            direction = pressPos.x - Input.mousePosition.x;
            transform.Rotate(new Vector3(0, direction * rotationSpeed, 0));
            pressPos = Input.mousePosition;
        }
        else
        {
            pressed = false;
        }
    }
}
