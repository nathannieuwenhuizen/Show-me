using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPGSlider : ARObject
{

    //ui thigs
    [SerializeField]
    private Image overlayImage;
    [SerializeField]
    private Image valueImage;
    [SerializeField]
    private Image valueImage2;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text expText;


    [SerializeField]
    private RectTransform lineParticleRect;
    private ParticleSystem lineParticle;

    //particles
    [SerializeField]
    private ParticleSystem levelParticles;

    private AudioSource audioS;

    //data
    private float currentValue = 45;
    private int level = 45;
    private bool animating = false;

    public bool activated = true;

    protected override void Start()
    {
        base.Start();
        lineParticle = lineParticleRect.GetComponent<ParticleSystem>();
        audioS = GetComponent<AudioSource>();
    }

    protected override void Focus()
    {
        levelText.gameObject.SetActive(true);
        expText.gameObject.SetActive(true);
        ChangeAlpha(1);
        valueImage.gameObject.SetActive(true);

        if (activated)
        {
            AnimateToLevel(47.8f);
        }

    }
    protected override void UnFocus()
    {
        levelText.gameObject.SetActive(false);
        expText.gameObject.SetActive(false);
        lineParticle.Stop();
        animating = false;
        StopAllCoroutines();
        valueImage.gameObject.SetActive(false);
        ChangeAlpha(0.4f);

    }

    private void ChangeAlpha(float val)
    {
        Color temp = overlayImage.color;
        temp.a = val;
        overlayImage.color = temp;
    }



    public void AnimateToLevel(float value)
    {
        if (!animating)
        {
            StartCoroutine(AnimatingToLevel(value));
        }
    }
    IEnumerator AnimatingToLevel(float value)
    {
        animating = true;
        lineParticle.Play();
        while( Mathf.Abs(CurrentValue - value) > 0.01f)
        {
            CurrentValue = Mathf.Lerp(currentValue, value, Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        CurrentValue = value;
        lineParticle.Stop();
        animating = false;
    }

    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            UpdateUI();
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            levelParticles.Play();
            audioS.Play();
            levelText.text = value + "";
        }
    }

    public void UpdateUI()
    {
        valueImage.fillAmount = CurrentValue % Mathf.Floor(CurrentValue);
        valueImage2.fillAmount = valueImage.fillAmount;
        lineParticleRect.localPosition = new Vector2(valueImage.rectTransform.rect.x + valueImage.rectTransform.rect.width * valueImage.fillAmount, 0);
        if (Mathf.Floor(CurrentValue) != Level)
        {
            audioS.pitch += 0.05f;
            Level++;
        }
    }
}
