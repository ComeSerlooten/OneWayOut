using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoorKeyLocks_UseEmptyHand : UseEmptyHand
{
    public int compteurActivation;

    private void Awake()
    {
        compteurActivation = 0;
    }

    public void LockOpened()
    {
        compteurActivation++;
        if(compteurActivation == 5)
        {
            canBeUsed = true;
        }
    }
}
