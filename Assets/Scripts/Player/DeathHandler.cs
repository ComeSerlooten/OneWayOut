using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPannel;
    public GameObject spawnPoint;
    public GameObject player;
    private Item[] items;

    private void Awake()
    {
        items = FindObjectsOfType(typeof(Item)) as Item[];
        //log :
        foreach(Item item in items)
        {
            Debug.Log("This item is in the list : " + item);
        }
    }

    public void PlayerDies(int indexOfEnding)
    {
        Debug.Log("Player died from end " + indexOfEnding);
        
       //On fait apparaitre un texte de mort
       SetEndingTitle(indexOfEnding);
       deathPannel.SetActive(true);
       //On fait rebouger tous les objets à leurs points de spawn 
       //Implique qu'on à une liste de tous les objets déplacable qqpart
       //Implique que les objets déplacable ait en mémoire leur position de départ
       //On laisse les passages ouvert ouvertF
       //on change rien de la map, que reset les grabbables
       ResetPositionOfAllObject();
       //on dit que la fin à été atteint
       //on fait en sorte que tous les objets mono utilisation lié a cette fin soit désactivé
       //je crois que c'est dejà fait par COME
       //on fait respawn le joueur dans la première salle
       ResetPlayerPosition();
    }

    private void SetEndingTitle(int indexOfEnding)
    {
        Debug.Log("Changing ending title of death pannel");
        deathPannel.GetComponentInChildren<TMP_Text>().text = "Vous avez atteint la fin n°" + indexOfEnding + "\nBonne chance pour trouver les suivantes !";
    }

    private void WaitForAllow()
    {
        Debug.Log("Waiting for player to close window of death");
        while (deathPannel.activeSelf); //tant que le panneau est actif on attend
        
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
    }
}
