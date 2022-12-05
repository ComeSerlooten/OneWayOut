using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabber))]
[RequireComponent(typeof(SelectorRay))]
public class PlayerUse : MonoBehaviour
{
    [SerializeField] Transform useItemPrompt;
    bool attemptableUse = false;
    bool itemCarried = false;

    [SerializeField] KeyCode keyToUse = KeyCode.E;

    Grabber grab;
    SelectorRay selector;


    private void Awake()
    {
        grab = GetComponent<Grabber>();        
        selector = GetComponent<SelectorRay>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GrabberUpdate(bool isDropping)
    {
        itemCarried = !isDropping;
        if (!itemCarried) useItemPrompt.gameObject.SetActive(false);
    }

    public bool TryUsing(GameObject first, GameObject second)
    {
        InteractableObject[] interactors = first.GetComponents<InteractableObject>();
        if(interactors.Length > 0)
        {
            foreach (InteractableObject interact in interactors)
            {
                if (interact.Use(second.GetComponent<Item>())) return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemCarried)
        {
            bool heldItems = (grab.grabbedObject && selector.inView);
            //attemptableUse = (grab.grabbedObject) ? (selector.inView ? (grab.grabbedObject.GetComponent<InteractableObject>() || selector.inView.GetComponent<InteractableObject>()) : false) : false ;
            attemptableUse = heldItems ? ((selector.inView.GetComponent<Item>()) ? (grab.grabbedObject.GetComponent<InteractableObject>() || selector.inView.GetComponent<InteractableObject>()) : false) : false;
            //if(grab.grabbedObject && selector.inView) if (selector.inView.GetComponent<InteractableObject>() == grab.grabbedObject.GetComponent<InteractableObject>()) attemptableUse = false;
            useItemPrompt.gameObject.SetActive(attemptableUse);

            if(attemptableUse && !grab.grabbedObject.GetComponent<ArcadeGun>())
            {
                if(Input.GetKeyDown(keyToUse))
                {
                    bool use = TryUsing(grab.grabbedObject.gameObject, selector.inView.gameObject);
                    if(!use) TryUsing(selector.inView.gameObject, grab.grabbedObject.gameObject);
                }
            }
        }
        else
        {
            if(selector.inView)
            {
                if(selector.inView.GetComponent<UseEmptyHand>())
                {
                    if(selector.inView.GetComponent<UseEmptyHand>().canBeUsed)
                    {
                        useItemPrompt.gameObject.SetActive(true);
                        if (Input.GetKeyDown(keyToUse))
                        {
                            selector.inView.GetComponent<UseEmptyHand>().Use();
                        }
                    }
                    else useItemPrompt.gameObject.SetActive(false);


                }
                else useItemPrompt.gameObject.SetActive(false);
            }
            else useItemPrompt.gameObject.SetActive(false);
        }
        
    }
}
