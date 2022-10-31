using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_BlockTest : InteractableObject
{
    public override bool ActivationCondition()
    {
        return true;
    }

    public override void ActivationSequence()
    {
        base.ActivationSequence();
        Debug.Log("Item " + name + " Activated!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
