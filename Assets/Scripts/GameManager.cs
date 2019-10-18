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

    public static GameManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    public void TargetIsFound()
    {
        onTargetFound?.Invoke();
        locater.Found();
    }
    public void TargetIsLost()
    {
        onTargetLost?.Invoke();
        locater.LocateTree();
    }


}
