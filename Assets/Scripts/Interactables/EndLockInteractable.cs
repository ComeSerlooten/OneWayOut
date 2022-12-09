using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndLockInteractable : InteractableObject
{
    [SerializeField] Transform outKeyPos;
    [SerializeField] Transform inKeyPos;
    [SerializeField] Transform lockedKeyPos;
    [SerializeField] Item key;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void PutKeyIn()
    {
        key.transform.DOMove(outKeyPos.position, 1f).SetEase(Ease.InOutSine);
        key.transform.DORotateQuaternion(outKeyPos.rotation, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            key.transform.DOMove(inKeyPos.position, .5f).SetEase(Ease.InOutSine);
            key.transform.DORotateQuaternion(inKeyPos.rotation, .5f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                key.transform.DOMove(lockedKeyPos.position, .5f).SetEase(Ease.InOutSine);
                key.transform.DORotateQuaternion(lockedKeyPos.rotation, .5f).SetEase(Ease.InOutSine).OnComplete(() =>
                {

                });
            });
        });
    }

    public override void ActivationSequence()
    {
        PutKeyIn();
        key.enabled = false;
        key.GetComponent<Collider>().enabled = false;
        key.GetComponent<Rigidbody>().isKinematic = true;
        key.GetComponent<Grabbable>().enabled = false;
        key.GetComponent<Glow>().enabled = false;
    }

    public override bool Use(Item user)
    {
        key = user;
        return base.Use(user);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
