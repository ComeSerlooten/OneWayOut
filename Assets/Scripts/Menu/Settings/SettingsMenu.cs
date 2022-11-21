using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropDown;

    public TextMeshProUGUI sensiTxt;
    private float sens;
    MouseLook mouseLook;

    private void Awake()
    {
        mouseLook = FindObjectOfType(typeof(MouseLook)) as MouseLook;
        sens = mouseLook.getSens();
    }

    void Start()
    {
        sens = 0.5f;
        sensiTxt.text = sens.ToString("0.00");

        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> resolutionsString = new List<string>();

        int curentResolutionIndex = 0;
        int index = 0;
        foreach(Resolution mRes in resolutions)
        {
            resolutionsString.Add(mRes.width + " x " + mRes.height);

            if (mRes.width == Screen.currentResolution.width && 
                mRes.height == Screen.currentResolution.height)
                curentResolutionIndex = index;

            index++;
        }

        resolutionDropDown.AddOptions(resolutionsString);
        resolutionDropDown.value = curentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        Debug.Log("Volume is set to : " + Mathf.Log10(volume) * 20 + "db");
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }

    public void SetFullScreen(bool enable)
    {
        Debug.Log("FullScreen is : " + enable);
        Screen.fullScreen = enable;
    }

    public void SetSensitivity(float value)
    {
        sens = value;
        Debug.Log("Sensitivity is set to : " + sens);
        sensiTxt.text = sens.ToString();
        mouseLook.setSensitivity(sens);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
