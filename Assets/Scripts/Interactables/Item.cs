using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    Vector3 initPosition;
    Quaternion initRotation;
    private bool initActive;

    private void Awake()
    {
        initPosition = transform.position;
        initRotation = transform.rotation;
        initActive = gameObject.activeSelf;
    }

    public void ResetItem()
    {
        Debug.Log(itemName);
        gameObject.SetActive(initActive);
        if(GetComponent<Rigidbody>())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (GetComponent<BoxCollider>())
        {
            BoxCollider bc = GetComponent<BoxCollider>();
            bc.enabled = true;
        }
        transform.position = initPosition;
        transform.rotation = initRotation;
    }

    public bool OnUse(Item subject)
    {
        InteractableObject[] interactions = subject.GetComponents<InteractableObject>();

        if (interactions.Length > 0)
        { 
            foreach (InteractableObject IO in interactions)
            {
                if(IO.target_ == this && IO.activatable && !(IO.triggerOnce &&IO.triggered))
                {
                    IO.TriggerSequence.Invoke();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}
