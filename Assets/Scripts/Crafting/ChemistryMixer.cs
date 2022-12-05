using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChemistryMixer : MonoBehaviour
{

    public Transform craftedObject;
    [SerializeField] Transform SpinnyPart;
    [SerializeField] Transform InsidePos;
    [SerializeField] Transform EndPos;
    bool initialCollider = false;
    bool initialRb = false;
    [Space]
    [SerializeField] Transform debugCrafted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("Craft")]
    public void DebugCrafted()
    {
        BringOutCraft(debugCrafted);
    }

    public void InitiateCraftSequence(CraftArea crafter)
    {
        BringOutCraft(crafter.craftedItem.transform);
    }

    public void BringOutCraft(Transform crafted)
    {
        craftedObject = crafted;

        if (craftedObject.GetComponent<Collider>())
        {
            initialCollider = craftedObject.GetComponent<Collider>().enabled;
            craftedObject.GetComponent<Collider>().enabled = false;
        }

        if (craftedObject.GetComponent<Rigidbody>())
        {
            initialRb = craftedObject.GetComponent<Collider>().enabled;
            craftedObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        craftedObject.position = InsidePos.position;
        craftedObject.gameObject.SetActive(false);
        Tween spin = SpinnyPart.DOLocalRotate(new Vector3(0, 0, 360 * 5), 3, RotateMode.FastBeyond360).SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            craftedObject.gameObject.SetActive(true);
            craftedObject.DOMove(EndPos.position, 2f).OnComplete(() =>
            {
                if (craftedObject.GetComponent<Collider>()) craftedObject.GetComponent<Collider>().enabled = initialCollider;
                if (craftedObject.GetComponent<Rigidbody>()) craftedObject.GetComponent<Rigidbody>().isKinematic = initialRb;
            });
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
