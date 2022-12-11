using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour


{

    [SerializeField]

    private float speed = 4f, curspeed;


    private Vector3 deplacement = Vector3.zero;

   
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {

            deplacement = Vector3.forward;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {

            deplacement = Vector3.back;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            deplacement = Vector3.zero;
            transform.Rotate(Vector3.up * -20 * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {

            deplacement = Vector3.zero;
            transform.Rotate(Vector3.up * 20 * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            curspeed = speed*20;
        }

            transform.Translate (deplacement * speed * Time.fixedDeltaTime);
        deplacement = Vector3.zero;
    }
}
