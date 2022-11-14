using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusHandler : MonoBehaviour
{
    [SerializeField] KeyCode keyForCarnet = KeyCode.Tab;
    [SerializeField] KeyCode keyForMenuPause = KeyCode.Escape;

    [SerializeField] GameObject carnet;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject menu;

    private void Update()
    {
        if (!carnet.activeSelf && !menu.activeSelf) //Les deux pas ouvert
        {
            if (Input.GetKeyDown(keyForCarnet)) //On veut ouvrir le carnet
            {
                OpenCarnet();
            }
            else if (Input.GetKeyDown(keyForMenuPause)) //On veut ouvrir le carnet
            {
                OpenMenu();
            }
        }
        else if (carnet.activeSelf) //Le carnet est ouvert
        {
            if (Input.GetKeyDown(keyForCarnet)) //On veut ouvrir le carnet
            {
                CloseCarnet();
            }
        }
        else if (menu.activeSelf) //Le menu est ouvert
        {
            if (Input.GetKeyDown(keyForMenuPause)) //On veut ouvrir le carnet
            {
                CloseMenu();
            }
        }
    }

    public void OpenCarnet()
    {
        Debug.Log("OpenCarnet()");
        carnet.SetActive(true);
        SetPause();
    }

    public void CloseCarnet()
    {
        Debug.Log("CloseCarnet()");
        carnet.SetActive(false);
        ReleasePause();
    }

    public void OpenMenu()
    {
        Debug.Log("OpenMenu()");
        menu.SetActive(true);
        SetPause();
    }


    public void CloseMenu()
    {
        if (!setting.activeSelf)
        {
            Debug.Log("CloseMenu()");
            menu.SetActive(false);
            ReleasePause();
        }
    }

    private void SetPause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ReleasePause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
