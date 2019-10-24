using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingScreen;

    [SerializeField]
    private Toggle vibrationToggle;

    [SerializeField]
    private Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {

        soundSlider.value = Settings.Volume;
        AudioListener.volume = Settings.Volume;

        vibrationToggle.isOn = Settings.Vibration;

        soundSlider.onValueChanged.AddListener(delegate {
            Settings.Volume = soundSlider.value;
        });
        vibrationToggle.onValueChanged.AddListener(delegate {
            Settings.Vibration = vibrationToggle.isOn;
        });
    }
    public void ToggleSettingScreen()
    {
        if (settingScreen.activeSelf == true)
        {
            settingScreen.SetActive(false);
        } else
        {
            settingScreen.SetActive(true);
        }
    }
}
