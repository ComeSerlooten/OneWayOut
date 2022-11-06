using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour

{
    public float rotatex;
    public float rotatey;
    public float sensity = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rotatex -= Input.GetAxis ("Mouse Y") * sensity;

        rotatey += Input.GetAxis ("Mouse X") * sensity;

        transform.rotation = Quaternion.Euler(rotatex,rotatey,0);
        
    }
}
