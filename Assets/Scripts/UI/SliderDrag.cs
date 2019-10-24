using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SliderDrag : MonoBehaviour, IPointerUpHandler
{

    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        audioSource.Play();
    }
}