using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] GameObject thithon;
    [SerializeField] GameObject whereTo;
   public void TeleportPlayerToVent()
    {
        thithon.GetComponent<CharacterController>().enabled = false;
        thithon.transform.position = whereTo.transform.position;
        thithon.GetComponent<CharacterController>().enabled = true;
        return;
    }
}
