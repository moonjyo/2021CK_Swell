using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("JihoScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        if(Application.isPlaying)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
