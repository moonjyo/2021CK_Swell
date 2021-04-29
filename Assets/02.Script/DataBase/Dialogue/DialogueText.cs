using System.Collections;
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

    public float ShowTimeSecond = 2f;

    public Dialogue[] CurrentDialogue;

    public Vector3 OffsetPosVec;

    public bool IsNextDialogue = false;
    public bool IsDialogue = false;

    public int TextCount = 0;

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
