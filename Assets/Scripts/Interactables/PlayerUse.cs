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
        if (!isDropping) useItemPrompt.gameObject.SetActive(false);
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
            attemptableUse = (grab.grabbedObject) ? (selector.inView ? (grab.grabbedObject.GetComponent<InteractableObject>() || selector.inView.GetComponent<InteractableObject>()) : false) : false;
            useItemPrompt.gameObject.SetActive(attemptableUse);
            

            if(attemptableUse)
            {
                if(Input.GetKeyDown(keyToUse))
                {
                    bool use = TryUsing(grab.grabbedObject.gameObject, selector.inView.gameObject);
                    if(!use) TryUsing(selector.inView.gameObject, grab.grabbedObject.gameObject);
                }
            }
        }
        
    }
}
