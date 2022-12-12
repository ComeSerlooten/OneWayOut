using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : Item
{
    Grabber grabber;
    public Item target_;
    public UnityEvent TriggerSequence;

    public bool activatable;
    public bool triggerOnce;
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        grabber = FindObjectOfType<Grabber>();
        StartCoroutine(CheckConditions());
        TriggerSequence.AddListener(grabber.ForceDrop);
    }

    public void SetActivatable(bool state)
    {
        activatable = state;
    }

    public virtual bool ActivationCondition()
    {
        return true;
    }

    IEnumerator CheckConditions()
    {
        activatable = ActivationCondition();
        yield return new WaitForSeconds(0.25f);
    }

    public virtual bool Use(Item user)
    {
        if((user == target_ || user.itemName == target_.itemName) && activatable && !(triggerOnce && triggered))
        {
            TriggerSequence.Invoke();
            ActivationSequence();
            triggered = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    virtual public void ActivationSequence()
    { }

}
