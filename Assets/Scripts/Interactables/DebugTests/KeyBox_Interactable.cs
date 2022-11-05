using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBox_Interactable : InteractableObject
{
    [SerializeField] GameObject keyPart;
    [SerializeField] KeyParts_Interactable keyTop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ActivationSequence()
    {
        GameObject go = Instantiate(keyPart, transform.position, Quaternion.identity);
        keyTop.keyBottom = go.transform;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
