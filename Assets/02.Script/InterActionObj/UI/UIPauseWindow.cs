﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPauseWindow : UIView
{
    public GameObject CreditPanel;
    public RawImage Credit;

    public void ShowCredit()
    {
        StartCoroutine(StartCredit());
    }


    public IEnumerator StartCredit()
    {
        CreditPanel.SetActive(true);
        Credit.rectTransform.anchoredPosition = new Vector2(0, -2570f);
        Credit.rectTransform.DOLocalMoveY(2570f, 20f).OnComplete(() => CreditPanel.SetActive(false)).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0f);
    }
}
