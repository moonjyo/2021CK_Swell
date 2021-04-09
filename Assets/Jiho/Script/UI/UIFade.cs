using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : UIView
{
    public RectTransform Circle;
    public RectTransform BackBlackUp;
    public RectTransform BackBlackDown;
    public RectTransform BackBlackRight;
    public RectTransform BackBlackLeft;

    public bool IsSceneMove = false;

    public IEnumerator SceneMoveOut()
    {
        while (Circle.sizeDelta.x >= 0)
        {
            Circle.sizeDelta -= new Vector2(80f, 80f);
            BackBlackUp.sizeDelta += new Vector2(0, 40f);
            BackBlackDown.sizeDelta += new Vector2(0, 40f);
            BackBlackLeft.sizeDelta += new Vector2(40f, 0);
            BackBlackRight.sizeDelta += new Vector2(40f, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator SceneMoveIn()
    {
        while (Circle.sizeDelta.x <= 5000)
        {
            Circle.sizeDelta += new Vector2(80f, 80f);
            BackBlackUp.sizeDelta -= new Vector2(0, 40f);
            BackBlackDown.sizeDelta -= new Vector2(0, 40f);
            BackBlackLeft.sizeDelta -= new Vector2(40f, 0);
            BackBlackRight.sizeDelta -= new Vector2(40f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        PlayerManager.Instance.playerMove.IsGravity = false;
        Debug.Log("endin");
        GameManager.Instance.uiManager.UIFade.Toggle(false);
        if (GameManager.Instance.stageManager.CurrentGetSceneName() == "Stage02")
        {
            GameManager.Instance.stageManager.stage2.StartStage2();
        }
    }
}
