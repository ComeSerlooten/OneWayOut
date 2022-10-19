using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingLoader : MonoBehaviour
{
    [SerializeField]
    private Ending[] endings;

    [SerializeField] private EndingLog log;

    private void Awake()
    {
        Debug.Log("Awake ending loader");
        foreach (Ending mEnding in endings)
        {
            Debug.Log(mEnding.myTitle);
            log.loadEnding(mEnding); 
        }
    }
}
