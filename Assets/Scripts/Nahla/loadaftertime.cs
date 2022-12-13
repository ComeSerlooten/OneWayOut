using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadaftertime : MonoBehaviour
{
    [SerializeField]

    private float delay = 10f;

   

    private float timeelapsed;
    // Update is called onprivete ce per frame
    private void Update()
    {
        timeelapsed += Time.deltaTime;

        if (timeelapsed > delay)
        {

            SceneManager.LoadScene("Menu");
        }
    }
}
