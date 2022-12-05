using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(SelectorRay))]
public class Grabber : MonoBehaviour
{
    [SerializeField] public Grabbable grabbedObject;
    [SerializeField] Transform cam;
    [SerializeField] Transform holdPositionner;
    [SerializeField] Transform grabItemPrompt;
    public bool canGrab = true;
    public bool isGrabbing = false;
    [Range(0, 5f)] public float grabDistance = 2;
    [Range(0, 5f)] public float throwStrength = 2;
    [Range(-.5f, .5f)] public float holdHeight = -.4f;

    public UnityEvent onGrab;
    public UnityEvent onDrop;

    private bool originalGravityState = false;
    public int holdingLayerIndex;
    int layerOfItem;

    [SerializeField] LayerMask holdPositionMask;
    float initialHoldDist;

    SelectorRay ray;

    // Start is called before the first frame update
    void Start()
    {
        //Pickup(grabbedObject);   
        ray = GetComponent<SelectorRay>();
        initialHoldDist = holdPositionner.localPosition.z;
    }

    void AdjustHoldPosition()
    {
        float holdDist = initialHoldDist;
        /*Vector3 toHoldPos = holdPositionner.position - cam.transform.position;
        RaycastHit[] collidersInFront = Physics.SphereCastAll(cam.transform.position, .25f, toHoldPos.normalized, holdDist, holdPositionMask);
        
        if(collidersInFront.Length > 1)
        {
            Debug.Log(collidersInFront[1].transform.name);
            holdDist = collidersInFront[1].distance * .9f;
        }
        Debug.DrawRay(cam.transform.position, holdDist * toHoldPos.normalized, Color.red, .1f);*/

        float holdPos = (cam.localRotation.eulerAngles.x > 90) ? holdHeight : holdHeight * (1 - cam.localRotation.eulerAngles.x / 90);
        holdPositionner.localPosition = new Vector3(holdPositionner.localPosition.x, holdPos, holdDist);
        
    }

    public void Drop(float throwForce = 0)
    {
        isGrabbing = false;
        grabbedObject.Dropped();

        foreach(Transform t in grabbedObject.GetComponentsInChildren<Transform>())
        {
            t.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(layerOfItem));
        }

        StopCoroutine(KeepInPlace());

        DOTween.Kill(grabbedObject);



        grabbedObject.rb.useGravity = originalGravityState;
        grabbedObject.rb.AddForce((cam.forward * 3 + Vector3.up*.5f) * (throwForce+0.5f), ForceMode.Impulse);
        
        foreach(Collider c in grabbedObject.GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
        
        grabbedObject = null;
        onDrop.Invoke();
    }

    public void Pickup(Grabbable grab)
    {
        if(grab.isGrabbable && !isGrabbing)
        {
            grabbedObject = grab;
            layerOfItem = grab.gameObject.layer;

            foreach (Transform t in grabbedObject.GetComponentsInChildren<Transform>())
            {
                t.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(holdingLayerIndex));
            }

            grab.isGrabbed = true;
            isGrabbing = true;
            originalGravityState = grab.rb.useGravity;
            grab.rb.useGravity = false;
            grab.transform.DOMove(holdPositionner.position, .25f).SetEase(Ease.InOutSine).OnComplete(() => { StartCoroutine(KeepInPlace()); });

            foreach (Collider c in grabbedObject.GetComponentsInChildren<Collider>())
            {
                c.enabled = false;
            }
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
                if (ray.inView.GetComponent<Grabbable>())
                {
                    grabItemPrompt.gameObject.SetActive(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        grabItemPrompt.gameObject.SetActive(false);
                        Vector3 toRaySelected = (ray.inView.transform.position - cam.transform.position);
                        if (toRaySelected.magnitude < grabDistance)
                        {
                            Pickup(ray.inView.GetComponent<Grabbable>());
                            onGrab.Invoke();
                        }
                    }
                }       
            }
            else
            {
                grabItemPrompt.gameObject.SetActive(false);
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




        AdjustHoldPosition();
    }
}
