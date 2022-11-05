using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseEmptyHand : MonoBehaviour
{
    public bool canBeUsed = true;
    public UnityEvent onUse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Use()
    {
        onUse.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
