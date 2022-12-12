using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketContentToggle : MonoBehaviour
{
    Grabbable grab;
    public bool filled;
    [Range(-1f, 1f)] public float spillAngleRatio = 0;
    [Space]
    [SerializeField] Transform water;
    [SerializeField] Transform dropPos;
    [SerializeField] GameObject splashParticles;
    [Space]
    [SerializeField] Transform playerCam;
    
    // Start is called before the first frame update
    void Start()
    {
        grab = GetComponent<Grabbable>();
        if (!playerCam) playerCam = Camera.main.transform;
    }

    public void Fill()
    {
        filled = true;

        water.gameObject.SetActive(true);
        water.gameObject.layer = this.gameObject.layer;

        GameObject part = Instantiate(splashParticles, dropPos.position, Quaternion.identity);
        part.layer = this.gameObject.layer;
    }

    public void Empty()
    {
        filled = false;

        water.gameObject.SetActive(false);
        water.gameObject.layer = this.gameObject.layer;

        GameObject part = Instantiate(splashParticles, dropPos.position, Quaternion.identity);
        part.layer = this.gameObject.layer;
    }

    [ContextMenu("Toggle")]
    public void ToggleState()
    {
        if (filled) Empty();
        else Fill();
    }


    void StayAligned()
    {
        transform.rotation = playerCam.rotation;

    }

    void SpillCheck()
    {
        float ratio = Vector3.Dot(Vector3.up, transform.up);

        if(grab.isGrabbed)
        {

        }

        if (ratio < spillAngleRatio) Empty();
    }

    // Update is called once per frame
    void Update()
    {
        if (grab.isGrabbed)
        {
            StayAligned();
        }

        if(filled) SpillCheck();
    }
}
