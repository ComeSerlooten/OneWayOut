using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baignoire_Interectable : InteractableObject
{
    [SerializeField] GameObject seauPlein;
    [SerializeField] GameObject seauVide;
    [SerializeField] Grabber grabber;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void ActivationSequence()
    {
        GameObject go = Instantiate(seauPlein, transform.position, Quaternion.identity);
        go.SetActive(true);
        grabber.Drop(0);
        Destroy(seauVide);
    }
}