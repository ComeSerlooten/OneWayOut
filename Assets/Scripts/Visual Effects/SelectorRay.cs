using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectorRay : MonoBehaviour
{
    [SerializeField] Transform cameraAim;
    [SerializeField] Transform itemLookedText;
    float maxDistance = 100;

    public GameObject inView;
    public bool objInView = false;

    public bool doRay = true;
    bool activeRay = false;
    [SerializeField] LayerMask rayLayers;

    // Start is called before the first frame update
    void Start()
    {
        inView = null;

        StartCoroutine(SearchForItem());
    }

    

    IEnumerator SearchForItem()
    {
        activeRay = true;
        while(doRay)
        {
            float dist = .1f;
            GameObject foundGlow = null;
            bool found = false;
            while (dist < maxDistance)
            {
                RaycastHit[] hits = Physics.SphereCastAll(cameraAim.position + cameraAim.forward*.5f, .1f, cameraAim.forward, dist, rayLayers);
                List<RaycastHit> l = new List<RaycastHit>();

                foreach (RaycastHit h in hits)
                {
                    if (h.transform.GetComponent<Glow>()) l.Add(h);
                }

                if (l.Count > 0)
                {
                    

                    if(l[0].transform.gameObject != null)
                    {
                        if (l[0].transform.gameObject.GetComponent<Glow>())
                        {
                            foundGlow = l[0].transform.gameObject;
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            break;
                        }
                    }
                }
                dist += .1f;
            }
            //Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, .25f);

            if (found)
            {
                if (foundGlow != inView)
                {
                    if (objInView) if (inView.GetComponent<Glow>()) inView.GetComponent<Glow>().glow = false;
                    inView = foundGlow;

                    if (inView.GetComponent<Item>())
                    {
                        itemLookedText.GetComponent<TextMeshProUGUI>().text = inView.GetComponent<Item>().itemName;
                        itemLookedText.gameObject.SetActive(true);
                    }
                    else itemLookedText.gameObject.SetActive(false);

                    inView.GetComponent<Glow>().observer = cameraAim;
                    //inView.GetComponent<Glow>().maxViewDistance = maxDistance;
                    inView.GetComponent<Glow>().glow = true;
                    objInView = true;
                }
            }
            else
            {
                itemLookedText.gameObject.SetActive(false);

                if(objInView) if (inView.GetComponent<Glow>()) inView.GetComponent<Glow>().glow = false;
                objInView = false;
                inView = null;
            }


            yield return new WaitForSeconds(.1f);
        }

        activeRay = false;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (doRay && !activeRay) StartCoroutine(SearchForItem());
    }
}
