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
        //On fait rebouger tous les objets � leurs points de spawn 
        //Implique qu'on � une liste de tous les objets d�placable qqpart
        //Implique que les objets d�placable ait en m�moire leur position de d�part
        //On laisse les passages ouvert ouvert
        //on change rien de la map, que reset les grabbables
        ResetPositionOfAllObject();
        //on dit que la fin � �t� atteint
        //on fait en sorte que tous les objets mono utilisation li� a cette fin soit d�sactiv�
        //je crois que c'est dej� fait par COME
        //on fait respawn le joueur dans la premi�re salle
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
