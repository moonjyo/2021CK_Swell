﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : UIView
{
    public RectTransform Circle;
    public RectTransform BackBlackUp;
    public RectTransform BackBlackDown;
    public RectTransform BackBlackRight;
    public RectTransform BackBlackLeft;

    bool IsSceneMove = false;

    public IEnumerator SceneMoveOut()
    {
        while (Circle.sizeDelta.x >= 0 && !IsSceneMove)
        {
            Circle.sizeDelta -= new Vector2(40f, 40f);
            BackBlackUp.sizeDelta += new Vector2(0, 20f);
            BackBlackDown.sizeDelta += new Vector2(0, 20f);
            BackBlackLeft.sizeDelta += new Vector2(20f, 0);
            BackBlackRight.sizeDelta += new Vector2(20f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        IsSceneMove = true;
        Debug.Log("endout");
    }

    public IEnumerator SceneMoveIn()
    {
        while (Circle.sizeDelta.x <= 5000 && IsSceneMove)
        {
            Circle.sizeDelta += new Vector2(40f, 40f);
            BackBlackUp.sizeDelta -= new Vector2(0, 20f);
            BackBlackDown.sizeDelta -= new Vector2(0, 20f);
            BackBlackLeft.sizeDelta -= new Vector2(20f, 0);
            BackBlackRight.sizeDelta -= new Vector2(20f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        IsSceneMove = false;
        Debug.Log("endin");
    }
}
