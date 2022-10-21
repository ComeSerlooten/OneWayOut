using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;


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
