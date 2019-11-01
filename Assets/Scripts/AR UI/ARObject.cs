using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    protected float angleLimit = 80;
    protected bool focused = false;

    protected float y_offset;
    private float frequency = .02f;
    private float index = 0;
    private float amplitude = 0.003f;

    protected virtual void Start()
    {
        index = Random.Range(0, Mathf.PI * 2);
    }
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
        FloatAnimation();
    }
    protected virtual void FloatAnimation()
    {
        index = (index + frequency) % (Mathf.PI * 2);
        y_offset = Mathf.Sin(index) * amplitude;
    }


    protected virtual void Focus()
    {

    }
    protected virtual void UnFocus()
    {

    }

}
