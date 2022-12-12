using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCam : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float endPosZ = 45;
    [SerializeField] Scene mainGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
        if (transform.position.z >= endPosZ) SceneManager.LoadScene(mainGame.name);
    }
}
