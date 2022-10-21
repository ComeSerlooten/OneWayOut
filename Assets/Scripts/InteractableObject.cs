using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : Item
{
    public Item target_;
    public UnityEvent TriggerSequence;

    public bool activatable;
    public bool triggerOnce;
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        CheckConditions();
    }

    public virtual bool ActivationCondition()
    {
        return false;
    }

    IEnumerator CheckConditions()
    {
        activatable = ActivationCondition();
        yield return new WaitForSeconds(0.25f);
    }

    void ActivationSequence()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
