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

    [SerializeField]
    private CameraFade cameraFade;
    // Start is called before the first frame update
    void Start()
    {
        cameraFade.alphaFadeValue = 1;
        cameraFade.fadingOut = false;

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

    public void Quit()
    {
        Application.Quit();
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
