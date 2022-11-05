using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyParts_Interactable : InteractableObject
{
    [SerializeField] public Transform keyBottom;
    [SerializeField] GameObject keyComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ActivationSequence()
    {
        Instantiate(keyComplete, transform.position, transform.rotation);
        keyBottom.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
