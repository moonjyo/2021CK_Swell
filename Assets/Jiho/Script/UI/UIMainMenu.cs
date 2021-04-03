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

    public RectTransform Circle;
    public RectTransform BackBlackUp;
    public RectTransform BackBlackDown;
    public RectTransform BackBlackRight;
    public RectTransform BackBlackLeft;

    //bool test1 = false;

    public void Start()
    {
        testtext.DOText("This is DOText testing code, 한국어", 3f, false, ScrambleMode.None, null);
        StartCoroutine(OnTyping(0.1f, "This is DOText testing code, 한국어"));
    }

    public IEnumerator SceneMoveOut()
    {
        while (Circle.sizeDelta.x >= 0)
        {
            Circle.sizeDelta -= new Vector2(40f, 40f);
            BackBlackUp.sizeDelta += new Vector2(0, 20f);
            BackBlackDown.sizeDelta += new Vector2(0, 20f);
            BackBlackLeft.sizeDelta += new Vector2(20f, 0);
            BackBlackRight.sizeDelta += new Vector2(20f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator SceneMoveIn()
    {
        while (Circle.sizeDelta.x <= 5000)
        {
            Circle.sizeDelta += new Vector2(40f, 40f);
            BackBlackUp.sizeDelta -= new Vector2(0, 20f);
            BackBlackDown.sizeDelta -= new Vector2(0, 20f);
            BackBlackLeft.sizeDelta -= new Vector2(20f, 0);
            BackBlackRight.sizeDelta -= new Vector2(20f, 0);
            yield return new WaitForSeconds(0.01f);
        }
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

    public void CircleScaleTrans()
    {
        Circle.transform.localScale -= new Vector3(2,0,0) * Time.deltaTime;
    }
   
}
