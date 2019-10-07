using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    //
    public void LoadLevelFromUI (int levelIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex);
    }
}
