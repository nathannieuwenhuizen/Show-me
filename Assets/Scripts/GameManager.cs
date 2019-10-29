using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void targetFound();
    public targetFound onTargetFound;

    public delegate void targetLost();
    public targetLost onTargetLost;

    [SerializeField]
    private LocatorNotification locater;

    [SerializeField]
    private RPGSlider rpgSlider;

    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void TargetIsFound()
    {
        onTargetFound?.Invoke();
        rpgSlider.activated = true;
        locater.Found();
    }
    public void TargetIsLost()
    {
        onTargetLost?.Invoke();
        rpgSlider.activated = false;
        locater.LocateTree();
    }


}
