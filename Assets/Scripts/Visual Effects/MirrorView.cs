using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorView : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Transform observer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 observerToCam = (cam.position - (observer.position - observer.forward*.25f)).normalized;
        Vector3 direction = Vector3.Reflect(observerToCam, -transform.forward);
        cam.position = new Vector3(cam.position.x, observer.position.y, cam.position.z);
        cam.forward = direction;
    }
}
