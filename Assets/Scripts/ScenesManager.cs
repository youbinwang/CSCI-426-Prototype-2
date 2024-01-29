using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            
            SceneManager.LoadScene(sceneName);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
