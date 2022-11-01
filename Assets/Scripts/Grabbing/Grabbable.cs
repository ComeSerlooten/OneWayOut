using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grabbable : MonoBehaviour
{
    public bool isGrabbable;
    public bool isGrabbed;
    public bool canInspect;

    public ObjectInspect inspecting;

    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Dropped()
    {
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
