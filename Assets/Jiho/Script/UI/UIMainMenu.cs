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

    public void Update()
    {
        //if (Circle.sizeDelta.x >= 0 && !test1)
        //{
        //    Circle.sizeDelta -= new Vector2(4f, 4f);
        //    BackBlackUp.sizeDelta += new Vector2(0, 2f);
        //    BackBlackDown.sizeDelta += new Vector2(0, 2f);
        //    BackBlackLeft.sizeDelta += new Vector2(2f, 0);
        //    BackBlackRight.sizeDelta += new Vector2(2f, 0);
        //}
        //else
        //    test1 = true;

        //if(test1)
        //{
        //    SceneMoveIn();
        //}
        int i = 0; 
        if(i == 0)
        {
            StartCoroutine(SceneMoveOut());
            i++;
        }
        
    }
    public IEnumerator SceneMoveOut()
    {
        while (Circle.sizeDelta.x >= 0)
        {
            Circle.sizeDelta -= new Vector2(4f, 4f);
            BackBlackUp.sizeDelta += new Vector2(0, 2f);
            BackBlackDown.sizeDelta += new Vector2(0, 2f);
            BackBlackLeft.sizeDelta += new Vector2(2f, 0);
            BackBlackRight.sizeDelta += new Vector2(2f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;

    }

    public IEnumerator SceneMoveIn()
    {
        while (Circle.sizeDelta.x <= 5000)
        {
            Circle.sizeDelta += new Vector2(4f, 4f);
            BackBlackUp.sizeDelta -= new Vector2(0, 2f);
            BackBlackDown.sizeDelta -= new Vector2(0, 2f);
            BackBlackLeft.sizeDelta -= new Vector2(2f, 0);
            BackBlackRight.sizeDelta -= new Vector2(2f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        yield return Circle.sizeDelta.x >= 5000;
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
