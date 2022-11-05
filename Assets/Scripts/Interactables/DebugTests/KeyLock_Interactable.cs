using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock_Interactable : InteractableObject
{
    public GameObject completedKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ActivationSequence()
    {
        StartCoroutine(DropAndDissapear());
        //completedKey.SetActive(false);
    }

    IEnumerator DropAndDissapear()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(.5f);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
