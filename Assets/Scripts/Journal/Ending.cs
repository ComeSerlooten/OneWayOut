using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Ending
{

    [SerializeField]
    private string titre;

    [SerializeField]
    private string description;

    [SerializeField]
    private string help;

    [SerializeField]
    private bool completed = false;

    public EndingScript myEndingScript { get; set; }

    public UnityEvent TriggerSequence;

    public string myTitle
    {
        get
        {
            return titre;
        }
        set
        {
            titre = value;
        }
    }

    internal void Complete()
    {
        completed = true;
        TriggerSequence.Invoke();
    }

    public string getDescription()
    {
        return description;
    }

    public string getHelp()
    {
        return help;
    }

    public bool isCompleted()
    {
        return completed;
    }
}
