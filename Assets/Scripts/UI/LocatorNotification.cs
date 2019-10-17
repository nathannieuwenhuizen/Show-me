using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocatorNotification : MonoBehaviour
{
    private Text text;
    private int dotIndex = 0;
    private readonly int maxDots = 3;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    IEnumerator Locating()
    {
        yield return new WaitForSeconds(0.2f);
        dotIndex += 1;
        if (dotIndex > maxDots)
        {
            dotIndex = 0;
        }

        text.text = "Locating tree";
        for (int i = 0; i < maxDots; i++)
        {
            if (i >= dotIndex)
            {
                text.text += " ";
            } else
            {
                text.text += ".";

            }
        }
        StartCoroutine(Locating());

    }
    public void LocateTree()
    {
        StartCoroutine(Locating());
    }
    public void Found()
    {
        dotIndex = maxDots;
        StopAllCoroutines();
        text.text = "Found tree!    ";
    }
}
