using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Digicode : MonoBehaviour
{
    [SerializeField] string correctCode = "0000";
    [SerializeField] GameObject codeCanvas;
    [SerializeField] GameObject codeText;
    public string currentCode = "";
    public bool electricityOn = true;
    public bool unlocked = false;
    public bool isShown = false;

    public UnityEvent onUnlock;

    // Start is called before the first frame update
    void Start()
    {
        Clear();
    }

    public void SetElectricity(bool state)
    {
        electricityOn = state;
    }

    public void Exit()
    {
        ReleasePause();
        isShown = false;
        codeCanvas.SetActive(false);
    }

    public void Open()
    {
        if(electricityOn)
        {
            SetPause();
            isShown = true;
            codeCanvas.SetActive(true);
        }
    }

    public void Clear()
    {
        currentCode = "";
        UpdateText();
    }

    public void Correct()
    {
        if (currentCode.Length == 4) CheckCode();
    }

    public void AddNumber(string val)
    {
        currentCode += val;
        if (currentCode.Length > 4) currentCode = currentCode.Substring(0, 4);

        UpdateText();
    }

    public void CheckCode()
    {
        if (currentCode == correctCode)
        {
            unlocked = true;
            onUnlock.Invoke();
        }
        currentCode = "";
        Exit();
    }

    void UpdateText()
    {
        codeText.GetComponent<TMP_Text>().text = currentCode;
    }

    public void SetPause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReleasePause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
