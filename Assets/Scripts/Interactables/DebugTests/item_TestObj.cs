using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_TestObj : Item
{
    [SerializeField] InteractableObject interactable;

    [ContextMenu("Use on Selected")]
    void Use()
    {
        bool used = interactable.Use(this);
        if (used) Debug.Log("Correctly used");
        else Debug.Log("Incorrect Use");
    }

    public void EventTester()
    {
        Debug.Log("Event Triggered!");
    }
}
