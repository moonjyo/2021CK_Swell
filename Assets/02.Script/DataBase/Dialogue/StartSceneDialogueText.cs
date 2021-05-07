using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartSceneDialogueText : MonoBehaviour , IDialogueText
{
    public TextMeshProUGUI TMPDialogue;
    public TextMeshProUGUI TMPOnNext;

    public Image StandingImageL;


    public float ShowTimeSecond = 0.3f;

    public Dialogue[] CurrentDialogue;

    public Vector3 OffsetPosVec;

    public bool IsNextDialogue = false;
    public bool IsDialogue = false;

    public int TextCount = 0;

    public StartSceneDialogueData dialoguedata;


   
    public void Init()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator SetText()
    {
        TMPDialogue.text = CurrentDialogue[TextCount].context[0];
        TMPDialogue.text = TMPDialogue.text.Replace("\\n", "\n"); //줄바꿈용
       
        StandingImageL.sprite = GameManager.Instance.uiManager.DialogueImageDicR[CurrentDialogue[TextCount].TextureL].sprite;
        
        gameObject.SetActive(true);

        DialogueDoText(TMPDialogue, ShowTimeSecond);

        yield return new WaitForSeconds(0f);
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
    }

    public void DialogueDoText(TextMeshProUGUI a_text, float a_duration)
    {
        a_text.maxVisibleCharacters = 0;
        DOTween.To(x => a_text.maxVisibleCharacters = (int)x, 0f, a_text.text.Length, a_duration).OnComplete(() => {
            IsNextDialogue = true;
            ++TextCount;
            TMPOnNext.gameObject.SetActive(true);
        });
    }

    public void StartDialogue()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f, -335f);
        CurrentDialogue = dialoguedata.GetDialogoue();
        ShowDialogue();
        StartCoroutine(SetText());
    }

}
