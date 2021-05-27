using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

enum TimerState
{
    None,
    Step1,
    Step2,
    Step3,
    Step4,
}

public class UITimer : UIView
{
    //public Image TimerProgressBar;
    public Slider SliderTimeProgressBar;

    public GameObject GameOverCanvas;

    float Timer = 0;

    TimerState timerState;

    bool IsTimeOver = false; // 타이머에서 시간이 다 되었을 때

    public bool IsGameOver = false;

    void Start()
    {
        //TimerProgressBar.fillAmount = 0.0f;

        SliderTimeProgressBar.value = 0.0f;
        timerState = TimerState.None;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GameManager.Instance.uiManager.IsTimePuase)
        {
            Timer += Time.deltaTime;
        }
        
        if (Timer <= 300.0f)
        {
            SliderTimeProgressBar.value = Timer / 300;

            if (SliderTimeProgressBar.value >= 1.0f && timerState == TimerState.Step3)
            {
                timerState = TimerState.Step4;
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_Timer4", PlayerManager.Instance.transform.position);
                ShowGameOverCanvas();
            }
            else if (SliderTimeProgressBar.value > 0.75f && timerState == TimerState.Step2)
            {
                timerState = TimerState.Step3;
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_Timer3", PlayerManager.Instance.transform.position);
            }
            else if (SliderTimeProgressBar.value > 0.5f && timerState == TimerState.Step1)
            {
                timerState = TimerState.Step2;
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_Timer2", PlayerManager.Instance.transform.position);
            }
            else if (SliderTimeProgressBar.value > 0.25f && timerState == TimerState.None)
            {
                timerState = TimerState.Step1;
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stage1/SFX_St1_Timer1", PlayerManager.Instance.transform.position);
            }
        }
        else
        {
            IsTimeOver = true;
        }
    }

    public void ShowGameOverCanvas()
    {
        GameOverCanvas.SetActive(true);

        Color color = GameOverCanvas.GetComponent<Image>().color;
        color.a = 1;
        GameOverCanvas.GetComponent<Image>().DOColor(color, 2.0f);
    }
}
