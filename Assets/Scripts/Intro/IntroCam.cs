using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCam : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float endPosZ = 45;
    [SerializeField] bool doSceneChange = false;
    [SerializeField] bool doMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doMove) transform.position += Vector3.forward * speed * Time.deltaTime;

        if (transform.position.z >= endPosZ && doMove)
        {
            if (doSceneChange) SceneManager.LoadScene("Assets/Scenes/Drafts/Baptiste.unity");
            else
            {
                doMove = false;
            }
        }
    }
}
