using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInCovers : MonoBehaviour
{
    public List<Transform> itemsToHide;
    [SerializeField] Grabber grabber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RemoveHeldItems()
    {
        List<Transform> tempList = itemsToHide;

        foreach (Transform t in tempList)
        {
            if(grabber.grabbedObject.transform == t)
            {
                itemsToHide.Remove(t);
                break;
            }
        }
    }

    public void HideUntouched()
    {
        foreach (Transform t in itemsToHide)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void ShowUntouched()
    {
        foreach (Transform t in itemsToHide)
        {
            t.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
