using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.InputSystem;

public class DialogueText : MonoBehaviour , IDialogueText
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

    public int TextStartCount = 0;

    public int TextEndCount = 0;

    public TestNpcDialougeData dialoguedata;

    public DialogueText dialogueText;

    public bool IsDialogueStart = false;

    private Color ActiveTrueColor = new Color(1, 1, 1);
    private Color ActiveFalseColor = new Color(0.2830189f, 0.2830189f, 0.2830189f);


    public void Init()
    {
        gameObject.SetActive(false);
        CurrentDialogue = DataBaseManager.Instance.dialogueData.GetDialogoue();
    }

    public void ShowDialogue()
    {
        gameObject.SetActive(true);
        StartCoroutine(SetText());
    }

    public IEnumerator SetText()
    {
        TMPOnNext.gameObject.SetActive(false);
        TMPName.text = CurrentDialogue[TextStartCount].name;
        TMPDialogue.text = CurrentDialogue[TextStartCount].context[0];
        TMPDialogue.text = TMPDialogue.text.Replace("\\n", "\n"); //줄바꿈용
        if(GameManager.Instance.uiManager.DialogueImageDicL.ContainsKey(CurrentDialogue[TextStartCount].TextureL))
        {
            StandingImageL.sprite = GameManager.Instance.uiManager.DialogueImageDicL[CurrentDialogue[TextStartCount].TextureL].sprite;
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
            ++TextStartCount; 
            TMPOnNext.gameObject.SetActive(true);
        });
    }

    public void StartDialogue()
    {
        IsDialogueStart = true;
        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f, -335f);
        CurrentDialogue = dialoguedata.GetDialogoue();
        ShowDialogue();
        StartCoroutine(SetText());
    }


    public void DialogueCount(int startcount , int endCount)
    {
        TextStartCount = startcount;
        TextEndCount = endCount;
    }


}
