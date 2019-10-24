using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hologram : MonoBehaviour
{
    [SerializeField]
    private GameObject contents;
    [SerializeField]
    private Transform backdrop;
    private Image backdropSpriteRenderer;
    void Start()
    {
        GameManager.instance.onTargetFound += Animate;
        GameManager.instance.onTargetLost += Disappear;

        backdropSpriteRenderer = backdrop.GetComponent<Image>();
    }
    private void Update()
    {
        float angle = Vector3.Angle(transform.forward, Vector3.forward);
        if (angle > 90)
        {
            ChangeAlpha(0.2f);
            //backdrop.gameObject.SetActive(false);
            contents.gameObject.SetActive(false);
        } else
        {
            ChangeAlpha(0.5f);
            //backdrop.gameObject.SetActive(true);
            contents.gameObject.SetActive(true);
        }
    }
    private void ChangeAlpha(float val)
    {
        Color temp = backdropSpriteRenderer.color;
        temp.a = val;
        backdropSpriteRenderer.color = temp;

    }

    private void OnDisable()
    {
        GameManager.instance.onTargetFound -= Animate;
        GameManager.instance.onTargetLost -= Disappear;
    }

    public void Disappear()
    {
        contents.SetActive(false);
        StopAllCoroutines();
    }

    public void Animate()
    {
        StartCoroutine(Animating());
    }
    IEnumerator Animating()
    {

        backdrop.localScale = new Vector3(0,0,0);
        float speed = 1f;
        float index = 0;
        while (index < 0.7)
        {
            backdrop.localScale = new Vector3(Snap(backdrop.localScale.x, 1f, index), 0.1f, 1);
            index += Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
         
        speed = 3f;
        index = 0; 
        while (index < 1)
        {
            backdrop.localScale = new Vector3(1, Snap2(backdrop.localScale.y, 1f, index), 1);
            index += Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        contents.SetActive(true);

    }
    public float Snap(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = Mathf.Sin(value * Mathf.PI * 0.5f);
        return start + (end - start) * value;
    }
    public float Snap2(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (1.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        float result = end * value;
        //Debug.Log("value = " + value);
        //Debug.Log("result = " + result);

        return result;
    }
}
