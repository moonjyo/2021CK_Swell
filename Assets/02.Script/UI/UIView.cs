using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    public RectTransform RootRect;
    private Vector3 OriginalPos;
    private bool isDialogues = false;

    protected void Awake()
    {
        OriginalPos = RootRect.localPosition;
    }

    public virtual void Initialize()
    {

    }

    public virtual void Toggle(bool value)
    {
        if(value)
        {
            GameManager.Instance.uiManager.PauseWindows.gameObject.SetActive(true);
            Time.timeScale = 0f;
            RootRect.localPosition = Vector3.zero;
        }
        else
        {
            Time.timeScale = 1;
        }

        RootRect.gameObject.SetActive(value);
    }
}
