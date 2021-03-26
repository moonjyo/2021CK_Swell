using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIMainMenu : UIView
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("JihoScene", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        if (Application.isPlaying)
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void ShowSettingOptionMenu()
    {
        UIManager.Instance.UISettingOptionMenu.Toggle(true);
    }
}
