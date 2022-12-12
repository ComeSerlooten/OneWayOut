using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etagere_Interectable : InteractableObject
{
    [SerializeField] GameObject toSpawn;
    [SerializeField] Grabber grabber;

    public override void ActivationSequence()
    {
        GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
        grabber.Drop(0);
        gameObject.SetActive(false);
    }
}
