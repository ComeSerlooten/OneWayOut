using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class ArcadeGun : MonoBehaviour
{
    Grabbable grab;
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform bulletExit;
    [Space]
    [SerializeField] Transform playerCam;
    [SerializeField] LayerMask ignoreShotDetect;
    [Space]
    [SerializeField] float maxDistance = 20;
    public Vector3 pointFound;
    // Start is called before the first frame update
    void Start()
    {
        grab = GetComponent<Grabbable>();
        if (!playerCam) playerCam = Camera.main.transform;
    }

    Vector3 GetPointToLook()
    {
        Vector3 camForward = playerCam.transform.forward;
        float dist = 0.1f;
        RaycastHit[] hits = Physics.RaycastAll(playerCam.position, playerCam.forward, dist, ignoreShotDetect);
        while(hits.Length <= 0 && dist < maxDistance)
        {
            hits = Physics.RaycastAll(playerCam.position, playerCam.forward * dist, ignoreShotDetect);
            dist += 0.1f;
        }
        

        if(hits.Length > 0)
        {
            //Debug.Log(hits[0].transform.name);
            pointFound = hits[0].point;
            //Debug.DrawRay(playerCam.position, pointFound - playerCam.position, Color.green, .1f);

            Vector3 length = playerCam.position - hits[0].point;

            if (length.magnitude > 1) return hits[0].point;
        }
        else
        {
            pointFound = Vector3.zero;
            //Debug.DrawRay(playerCam.position, playerCam.forward * dist, Color.red, .1f);
            return playerCam.position + playerCam.forward * maxDistance;
        }


        return playerCam.position + playerCam.forward * maxDistance; ;
    }

    

    void StayAligned()
    {
        transform.rotation = playerCam.rotation;
        transform.LookAt(GetPointToLook());
        
    }

    // Update is called once per frame
    void Update()
    {

        if (grab.isGrabbed)
        {
            StayAligned();
            if (Input.GetKeyDown(KeyCode.E)) Instantiate(prefabBullet, bulletExit.position, transform.rotation);
        }
        else pointFound = Vector3.zero;

    }
}
