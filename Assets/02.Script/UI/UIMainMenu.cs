using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIMainMenu : UIView
{
    public Text testtext;
    public TextMeshProUGUI test;
    public GameObject StartButton;
    public GameObject ExitButton;

    public void Start()
    {
        //testtext.DOText("This is DOText testing code, 한국어", 3f, false, ScrambleMode.None, null);
        //StartCoroutine(OnTyping(0.1f, "This is DOText testing code, 한국어"));
    }


    public void StartGame()
    {
        StartCoroutine(ChangeScene());


    }

    IEnumerator ChangeScene()
    {
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        StartCoroutine(EnterStage());
    }

    IEnumerator EnterStage()
    {
        StartButton.SetActive(false);
        ExitButton.SetActive(false);
        yield return StartCoroutine(GameManager.Instance.stageManager.SceneChange("Stage02"));
        GameManager.Instance.stageManager.stage2.StartStage2();
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        GameManager.Instance.stageManager.stage2.gameObject.SetActive(true);
        GameManager.Instance.uiManager.UIMainMenu.Toggle(false);
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
        GameManager.Instance.uiManager.UISettingOptionMenu.Toggle(true);
    }

    //IEnumerator OnTyping(float interval, string Say)
    //{
    //    foreach(char item in Say)
    //    {
    //        test.text += item;
    //        yield return new WaitForSeconds(interval);
    //    }
    //}   
}
