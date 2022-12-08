using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] GameObject thithon;
    [SerializeField] DeathHandler deathHandler;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == thithon)
        {
            deathHandler.PlayerDies(3);
        }
    }
}
