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
        //log :
        foreach (Item item in items)
        {
            Debug.Log("This item is in the list : " + item);
        }
    }

    public void PlayerDies(int indexOfEnding)
    {
        Debug.Log("Player died from end " + indexOfEnding);

        //On fait apparaitre un texte de mort
        SetEndingTitle(indexOfEnding);
        menusHandler.OpenDeathPannel(indexOfEnding);
        //On fait rebouger tous les objets � leurs points de spawn 
        //Implique qu'on � une liste de tous les objets d�placable qqpart
        //Implique que les objets d�placable ait en m�moire leur position de d�part
        //On laisse les passages ouvert ouvertF
        //on change rien de la map, que reset les grabbables
        ResetPositionOfAllObject();
        //on dit que la fin � �t� atteint
        //on fait en sorte que tous les objets mono utilisation li� a cette fin soit d�sactiv�
        //je crois que c'est dej� fait par COME
        //on fait respawn le joueur dans la premi�re salle
        ResetPlayerPosition();
    }

    private void SetEndingTitle(int indexOfEnding)
    {
        Debug.Log("Changing ending title of death pannel");
        
    }

    private void ResetPositionOfAllObject()
    {
        Debug.Log("reseting position of all items");
        foreach (Item item in items)
        {
            Debug.Log("Reset item : " + item.itemName);
            item.ResetItem();
        }
    }

    private void ResetPlayerPosition()
    {
        Debug.Log("reseting position of the player");
        player.transform.position = spawnPoint.transform.position;
        //VOir si le player controller bloque pas �a
    }
}