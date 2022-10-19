using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingScript : MonoBehaviour {

    public Ending myEnding { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void select()
    {
        Debug.Log("Select");
        GetComponent<TextMeshProUGUI>().color = Color.red;
        EndingLog.myInstance.showDescription(myEnding);
    }

    public void deSelect()
    {
        Debug.Log("Delesect");
        GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
