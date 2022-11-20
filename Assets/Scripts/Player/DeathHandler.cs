using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathHandler : MonoBehaviour
{
    public MenusHandler menusHandler;
    public GameObject spawnPoint;
    public GameObject player;
    private Item[] items;

    private void Awake()
    {
        items = FindObjectsOfType(typeof(Item)) as Item[];
    }

    public void PlayerDies(int indexOfEnding)
    {
        Debug.Log("Player died from end " + indexOfEnding);

        //On fait apparaitre un texte de mort
        SetEndingTitle(indexOfEnding);
        menusHandler.OpenDeathPannel(indexOfEnding);
        //On fait rebouger tous les objets à leurs points de spawn 
        //Implique qu'on à une liste de tous les objets déplacable qqpart
        //Implique que les objets déplacable ait en mémoire leur position de départ
        //On laisse les passages ouvert ouvertF
        //on change rien de la map, que reset les grabbables
        ResetAllObject();
        //on dit que la fin à été atteint
        //on fait en sorte que tous les objets mono utilisation lié a cette fin soit désactivé
        //je crois que c'est dejà fait par COME
        //on fait respawn le joueur dans la première salle
        ResetPlayerPosition();
    }

    private void SetEndingTitle(int indexOfEnding)
    {
        Debug.Log("Changing ending title of death pannel");
    }

    private void ResetAllObject()
    {
        Debug.Log("reseting position of all items");
        foreach (Item item in items)
        {
            if (item.IsResetable())
            {
                Debug.Log("Reset item : " + item.itemName);
                item.ResetItem();
            }
            else
            {
                Debug.Log("Delete item : " + item.itemName);
                item.DeleteItem();
            }
            
        }
    }

    private void ResetPlayerPosition()
    {
        Debug.Log("reseting position of the player");
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        //VOir si le player controller bloque pas ça
    }
}