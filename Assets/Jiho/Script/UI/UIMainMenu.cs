using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIMainMenu : UIView
{
    public Text testtext;
    public TextMeshProUGUI test;
    public void Start()
    {
        testtext.DOText("This is DOText testing code", 3f, false, ScrambleMode.None, null);
        StartCoroutine(OnTyping(0.1f, "This is DOText testing code"));
    }
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

    IEnumerator OnTyping(float interval, string Say)
    {
        foreach(char item in Say)
        {
            test.text += item;
            yield return new WaitForSeconds(interval);
        }
    }
   
}
