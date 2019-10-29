using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    protected float angleLimit = 90;
    private bool focused = false;
    private void Update()
    {
        float angle = Vector3.Angle(transform.forward, Vector3.forward);
        if (angle > angleLimit)
        {
            if (focused)
            {
                focused = false;
                UnFocus();
            }
        }
        else
        {
            if (!focused)
            {
                focused = true;
                Focus();
            }
        }
    }
    protected virtual void Focus()
    {

    }
    protected virtual void UnFocus()
    {

    }

}
