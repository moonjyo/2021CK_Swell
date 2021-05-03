﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueText : MonoBehaviour
{
    public TextMeshProUGUI TMPDialogue;
    public TextMeshProUGUI TMPName;
    public TextMeshProUGUI TMPOnNext;

    public Image StandingImageL;
    public Image StandingImageR;

    public Image TalkTaleL;
    public Image TalkTaleR;


    public float ShowTimeSecond = 2f;

    public Dialogue[] CurrentDialogue;

    public Vector3 OffsetPosVec;

    public bool IsNextDialogue = false;
    public bool IsDialogue = false;

    public int TextCount = 0;

    private Color ActiveTrueColor = new Color(1, 1, 1);
    private Color ActiveFalseColor = new Color(0.2830189f, 0.2830189f, 0.2830189f);

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    public IEnumerator SetText()
    {   
        TMPOnNext.gameObject.SetActive(false);
        TMPName.text = CurrentDialogue[TextCount].name;
        TMPDialogue.text = CurrentDialogue[TextCount].context[0];
        TMPDialogue.text = TMPDialogue.text.Replace("\\n", "\n"); //줄바꿈용
        if(GameManager.Instance.uiManager.DialogueImageDicL.ContainsKey(CurrentDialogue[TextCount].TextureL))
        {
            StandingImageL.sprite = GameManager.Instance.uiManager.DialogueImageDicL[CurrentDialogue[TextCount].TextureL].sprite;
        }
        if (GameManager.Instance.uiManager.DialogueImageDicR.ContainsKey(CurrentDialogue[TextCount].TextureR))
        {
            StandingImageR.sprite = GameManager.Instance.uiManager.DialogueImageDicR[CurrentDialogue[TextCount].TextureR].sprite;
        }

        if (CurrentDialogue[TextCount].CurrentTurn == "L") //말차례턴 정해
        {
            StandingImageL.color = ActiveTrueColor;
            StandingImageR.color = ActiveFalseColor;
            TalkTaleL.gameObject.SetActive(true);
            TalkTaleR.gameObject.SetActive(false);
        }
        else
        {
            StandingImageR.color = ActiveTrueColor;
            StandingImageL.color = ActiveFalseColor; 
            TalkTaleL.gameObject.SetActive(false);
            TalkTaleR.gameObject.SetActive(true);
        }
        gameObject.SetActive(true); 

        DialogueDoText(TMPDialogue, ShowTimeSecond);
    
        yield return new WaitForSeconds(0f);
    }

    //TMP 지원은 Dotween에서 유로사용해야 DoText를 지원해주기 떄문에 임시로 만듬
    public void DialogueDoText(TextMeshProUGUI a_text, float a_duration)
    {
        a_text.maxVisibleCharacters = 0;
        DOTween.To(x => a_text.maxVisibleCharacters = (int)x, 0f, a_text.text.Length, a_duration).OnComplete(() => {
            IsNextDialogue = true;
            ++TextCount; 
            TMPOnNext.gameObject.SetActive(true);
        });
    }
}