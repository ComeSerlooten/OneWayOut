using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SelectorRay))]
public class Grabber : MonoBehaviour
{
    [SerializeField] Grabbable grabbedObject;
    [SerializeField] Transform cam;
    [SerializeField] Transform holdPositionner;
    [SerializeField] Canvas itemUseCanvas;
    public bool canGrab = true;
    public bool isGrabbing = false;
    [Range(0, 5f)] public float grabDistance = 2;
    [Range(0, 5f)] public float throwStrength = 2;


    private bool originalGravityState = false;

    public float debugForceThrow = 1;
    public bool debugToggleHold = false;
    public Grabbable debugItem;

    SelectorRay ray;

    // Start is called before the first frame update
    void Start()
    {
        //Pickup(grabbedObject);   
        ray = GetComponent<SelectorRay>();
    }

    void Drop(float throwForce = 0)
    {   
        isGrabbing = false;
        grabbedObject.Dropped();
        grabbedObject.rb.useGravity = originalGravityState;
        grabbedObject.rb.AddForce((cam.forward * 3 + Vector3.up) * (throwForce+0.5f), ForceMode.Impulse);

        StopCoroutine(KeepInPlace());
        grabbedObject.GetComponent<Collider>().enabled = true;
        grabbedObject = null;
    }

    void Pickup(Grabbable grab)
    {
        if(grab.isGrabbable && !isGrabbing)
        {
            grabbedObject = grab;
            isGrabbing = true;
            originalGravityState = grab.rb.useGravity;
            grab.rb.useGravity = false;
            grab.transform.DOMove(holdPositionner.position, .25f).SetEase(Ease.InOutSine).OnComplete(() => { StartCoroutine(KeepInPlace()); });
            grab.GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator KeepInPlace()
    {
        while(isGrabbing)
        {
            //grabbedObject.rb.MovePosition(holdPositionner.position);
            //grabbedObject.rb.velocity = grabbedObject.rb.velocity * .99f;
            //grabbedObject.rb.AddForce((holdPositionner.position - grabbedObject.transform.position),  ForceMode.Impulse);
            //if((grabbedObject.transform.position - holdPositionner.position).magnitude > 0.1f)
            //grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, holdPositionner.position, .1f);
            //if ((grabbedObject.transform.position - holdPositionner.position).magnitude > 0.025f)
            grabbedObject.transform.DOMove(holdPositionner.position, .05f).SetEase(Ease.Linear);
            grabbedObject.rb.velocity = Vector3.zero;
            grabbedObject.rb.angularVelocity = grabbedObject.rb.angularVelocity * .999f;

            yield return new WaitForSeconds(.05f);
        }

        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if(canGrab)
        {
            if (!isGrabbing && ray.inView != null)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Vector3 toRaySelected = (ray.inView.transform.position - cam.transform.position);
                    if (toRaySelected.magnitude < grabDistance)
                    {
                        Pickup(ray.inView.GetComponent<Grabbable>());
                    }
                }               
            }
            else
            {
                if (Input.GetMouseButtonDown(0) && grabbedObject != null)
                {
                    Drop(0);
                }
                else if (Input.GetMouseButtonDown(1) && grabbedObject != null)
                {
                    Drop(throwStrength);
                }
            }
        }

    }
}
