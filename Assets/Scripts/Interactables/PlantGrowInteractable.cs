using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlantGrowInteractable : InteractableObject
{
    [SerializeField] Transform grownPlantPos;
    [SerializeField] Vector3 grownScale;
    [SerializeField] float growthTime;
    [Space]
    [SerializeField] Transform regularPlant;

    public UnityEvent growthDone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void ActivationSequence()
    {
        Debug.Log("Activate");
        regularPlant.DOScale(grownScale, growthTime).SetEase(Ease.InOutSine);
        regularPlant.DOLocalMove(grownPlantPos.localPosition, growthTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            Debug.Log("Done");
            growthDone.Invoke();
        });
    }

    public override bool Use(Item user)
    {
        if ((user == target_ || user.itemName == target_.itemName) && activatable && !(triggerOnce && triggered))
        {
            if(user.GetComponent<BucketContentToggle>())
                if(user.GetComponent<BucketContentToggle>().filled)
                {
                    TriggerSequence.Invoke();
                    ActivationSequence();
                    user.GetComponent<BucketContentToggle>().Empty();
                    triggered = true;
                    return true;
                }

            return false;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
