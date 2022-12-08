using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenusHandler : MonoBehaviour
{
    [SerializeField] KeyCode keyForCarnet = KeyCode.Tab;
    [SerializeField] KeyCode keyForMenuPause = KeyCode.Escape;

    [SerializeField] GameObject carnet;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject deathPannel;

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

    public void OpenDeathPannel(int indexOfEnd)
    {
        Debug.Log("OpenDeathPannel(" + indexOfEnd + ")");
        if (!deathPannel.activeSelf)
        {
            Debug.Log("DeathPannel");
            deathPannel.GetComponentInChildren<TMP_Text>().text = "Vous avez atteint la fin n°" + indexOfEnd + "\nBonne chance pour trouver les suivantes !";
            deathPannel.SetActive(true);
            SetPause();
        }
    }

    public void CloseDeathPannel()
    {
        if (deathPannel.activeSelf)
        {
            Debug.Log("CloseDeathPannel()");
            deathPannel.SetActive(false);
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
