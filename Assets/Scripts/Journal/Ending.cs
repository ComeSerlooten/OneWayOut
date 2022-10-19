using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ending
{

    [SerializeField]
    private string titre;

    [SerializeField]
    private string description;

    [SerializeField]
    private string help;

    private bool completed = false;

    public EndingScript myEndingScript { get; set; }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
