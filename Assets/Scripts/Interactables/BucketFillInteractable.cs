using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketFillInteractable : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool Use(Item user)
    {
        if ((user == target_ || user.itemName == target_.itemName) && activatable && !(triggerOnce && triggered))
        {
            if (user.GetComponent<BucketContentToggle>())
                if (!user.GetComponent<BucketContentToggle>().filled)
                {
                    TriggerSequence.Invoke();
                    ActivationSequence();
                    user.GetComponent<BucketContentToggle>().Fill();
                    triggered = true;
                    return true;
                }

            return false;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
