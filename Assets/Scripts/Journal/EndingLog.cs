using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingLog : MonoBehaviour
{
    [SerializeField]
    private GameObject endingPrefab;

    [SerializeField]
    private Transform endingListParent;

    [SerializeField]
    private TextMeshProUGUI descriptionOfEnding;

    private Ending selectedEnding;

    private static EndingLog instance;

    public static EndingLog myInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<EndingLog>();
            }
            return instance;
        }
    }

    public void loadEnding(Ending ending)
    {
        GameObject go = Instantiate(endingPrefab, endingListParent);

        EndingScript es = go.GetComponent<EndingScript>();
        es.myEnding = ending;
        ending.myEndingScript = es;
        


        go.GetComponent<TextMeshProUGUI>().text = ending.myTitle;
    }

    public void showDescription(Ending ending)
    {
        if(selectedEnding != null){
            selectedEnding.myEndingScript.deSelect();
        }

        selectedEnding = ending;
        if (ending.isCompleted())
            descriptionOfEnding.text = ending.getDescription();
        else
            descriptionOfEnding.text = ending.getHelp();
    }
}
