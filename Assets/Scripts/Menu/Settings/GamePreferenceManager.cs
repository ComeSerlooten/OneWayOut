using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GamePreferenceManager : MonoBehaviour
{

    const string ResolutionKey = "Resolution";
    const string FullScreenKey = "FullScreen";
    const string GraphicKey = "Graphic";
    const string VolumeKey = "Volume";
    const string SensitivityKey = "Sensitivity";

    public TMPro.TMP_Dropdown resolutionDropDown;
    public Toggle fullScreenToggle;
    public TMPro.TMP_Dropdown graphicDropDown;
    public AudioMixer audioMixer;
    public Slider sliderVolume;//set
    public Slider sliderSens;

    public SettingsMenu settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }

    void ApplySave()
    {
        SavePrefs();
    }

    public void SavePrefs()
    {
        Debug.Log("SAVING PLAYER PREF");
        PlayerPrefs.SetInt(ResolutionKey, resolutionDropDown.value);
        PlayerPrefs.SetInt(FullScreenKey, Convert.ToInt32(fullScreenToggle.isOn));
        PlayerPrefs.SetInt(GraphicKey, graphicDropDown.value);
        float volume;
        audioMixer.GetFloat("MainVolume", out volume); //Mathf.Log10(volume) * 20
        volume = Mathf.Pow(10, volume / 20);
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.SetFloat(SensitivityKey, (float)sliderSens.value);
        Debug.Log("SAVED PLAYER PREF");
    }

    public void LoadPrefs()
    {
        int intBuffer;
        float floatBuffer;
        bool boolBuffer;

        settingsMenu.init();

        intBuffer = PlayerPrefs.GetInt(ResolutionKey, 0); //RES
        Debug.Log("Res : " + intBuffer);
        settingsMenu.SetResolution(intBuffer);
        resolutionDropDown.value = intBuffer;

        intBuffer = PlayerPrefs.GetInt(GraphicKey, 1);
        Debug.Log("Graphics : " + intBuffer);
        settingsMenu.SetGraphics(intBuffer);
        graphicDropDown.value = intBuffer;

        intBuffer = PlayerPrefs.GetInt(FullScreenKey, 1); //FULL
        if (intBuffer == 0) //false
        {
            boolBuffer = false;
        }
        else
        {
            boolBuffer = true;
        }
        Debug.Log("FullScreen : " + boolBuffer);
        fullScreenToggle.isOn = boolBuffer;
        settingsMenu.SetFullScreen(boolBuffer);

        floatBuffer = PlayerPrefs.GetFloat(VolumeKey, 0); //Audio
        Debug.Log("Volume : " + floatBuffer);
        settingsMenu.SetVolume(floatBuffer);
        sliderVolume.value = floatBuffer;

        floatBuffer = PlayerPrefs.GetFloat(SensitivityKey, 1000f); //Sensi
        Debug.Log("Sensi : " + floatBuffer);
        settingsMenu.SetSensitivity(floatBuffer);
        sliderSens.value = floatBuffer;
    }
}
