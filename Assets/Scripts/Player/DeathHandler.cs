using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public GameObject deathPannel;
    public GameObject spawnPoint;
    public GameObject player;
    private Grabbable[] grabbables;

    private void Awake()
    {
        grabbables = FindObjectsOfType(typeof(Grabbable)) as Grabbable[];
        //log :
        foreach(Grabbable grabbable in grabbables)
        {
            Debug.Log("This item is in the list : " + grabbable);
        }
    }

    public void PlayerDies()
    {
        //On fait apparaitre un texte de mort
        deathPannel.SetActive(true);
        WaitForAllow();
        //On fait rebouger tous les objets à leurs points de spawn 
        //Implique qu'on à une liste de tous les objets déplacable qqpart
        //Implique que les objets déplacable ait en mémoire leur position de départ
        //On laisse les passages ouvert ouvert
        //on change rien de la map, que reset les grabbables
        ResetPositionOfAllObject();
        //on dit que la fin à été atteint
        //on fait en sorte que tous les objets mono utilisation lié a cette fin soit désactivé
        //je crois que c'est dejà fait par COME
        //on fait respawn le joueur dans la première salle
        ResetPlayerPosition();
    }

    private void WaitForAllow()
    {
        while (deathPannel.activeSelf); //tant que le panneau est actif on attend
    }

    private void ResetPositionOfAllObject()
    {
        foreach (Grabbable grabbable in grabbables)
        {
            //grabbable.transform.position = grabbable.originalPos; 
        }
    }

    private void ResetPlayerPosition()
    {
        player.transform.position = spawnPoint.transform.position;
    }
}
