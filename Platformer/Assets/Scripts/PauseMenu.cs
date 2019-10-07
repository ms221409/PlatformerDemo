using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            menuObject.SetActive(!menuObject.activeSelf);
        }
    }


    public void LoadMainMenu ()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
