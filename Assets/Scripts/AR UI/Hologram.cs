using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hologram : ARObject
{
    [SerializeField]
    private GameObject contents;
    [SerializeField]
    private Transform backdrop;
    private Image backdropSpriteRenderer;
    private ParticleSystem focusParticle;

    private bool animating = false;
    protected override void Start()
    {
        GameManager.instance.onTargetFound += Animate;
        GameManager.instance.onTargetLost += Disappear;

        base.Start();
        backdropSpriteRenderer = backdrop.GetComponent<Image>();
        focusParticle = backdropSpriteRenderer.GetComponent<ParticleSystem>();
        //Animate();
    }
    protected override void Focus()
    {
        ChangeAlpha(0.8f);
        if (!animating)
        {
            contents.gameObject.SetActive(true);
            focusParticle.Play();
        }
    }
    protected override void UnFocus()
    {
        ChangeAlpha(0.3f);
        contents.gameObject.SetActive(false);
    }

    private void ChangeAlpha(float val)
    {
        Color temp = backdropSpriteRenderer.color;
        temp.a = val;
        backdropSpriteRenderer.color = temp;
    }

    protected override void FloatAnimation()
    {
        base.FloatAnimation();
        Vector3 temp = backdrop.transform.position;
        temp.y += y_offset;
        backdrop.transform.position = temp;

        temp = contents.transform.position;
        temp.y += y_offset;
        contents.transform.position = temp;
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
        animating = true;
        float endScale = backdrop.localScale.x;
        contents.SetActive(false);
        backdrop.localScale = new Vector3(0,0,0);
        float speed = 1f;
        float index = 0;
        while (index < 0.7)
        {
            backdrop.localScale = new Vector3(Snap(backdrop.localScale.x, endScale, index), 0.1f, endScale);
            index += Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
         
        speed = 3f;
        index = 0; 
        while (index < 1)
        {
            backdrop.localScale = new Vector3(endScale, Snap2(backdrop.localScale.y, endScale, index), endScale);
            index += Time.deltaTime * speed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (focused)
        {
            contents.SetActive(true);
        }
        animating = false;

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
        return result;
    }
}
