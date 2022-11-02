using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Entered");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Collider Left");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
