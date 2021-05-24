using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    float Timer = 0;

    TimerState timerState;

    bool IsTimeOver = false; // 타이머에서 시간이 다 되었을 때

    void Start()
    {
        //TimerProgressBar.fillAmount = 0.0f;

        SliderTimeProgressBar.value = 0.0f;
        timerState = TimerState.None;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Timer += Time.deltaTime;
        if (Timer <= 300.0f)
        {
            //TimerProgressBar.fillAmount = Timer / 30f;
            SliderTimeProgressBar.value = Timer / 300f;
            //switch(TimerProgressBar.fillAmount)
            //{
            //    case 0.25f: // 첫번재 칸 => 끼익, 엔진소리(할머니가 옴)

            //        break;
            //    case 0.5f: // 두번째 칸 => 차문 닫기, 엔진 꺼짐 소리(할머니가 차에서 내림)

            //        break;

            //    case 0.75f: // 세번째 칸 => 풀 밟는 소리

            //        break;

            //    case 1.0f: // 5분이 지남 => 문열기? (진행도에 따른 bool값 if에 부여)

            //        break;
            //}
            if (SliderTimeProgressBar.value >= 1.0f && timerState == TimerState.Step3)
            {
                timerState = TimerState.Step4;
            }
            else if (SliderTimeProgressBar.value > 0.75f && timerState == TimerState.Step2)
            {
                timerState = TimerState.Step3;
            }
            else if (SliderTimeProgressBar.value > 0.5f && timerState == TimerState.Step1)
            {
                timerState = TimerState.Step2;
            }
            else if (SliderTimeProgressBar.value > 0.25f && timerState == TimerState.None)
            {
                timerState = TimerState.Step1;
            }

        }
        else
        {
            IsTimeOver = true;
        }
            
    }
}
