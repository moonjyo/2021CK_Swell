using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class MonologueText : MonoBehaviour
{
    public TextMeshProUGUI TMPText;

    public MonologueData MonologueData; // script는 바뀔수있음.

    public float ShowTimeSecond = 2f;

    public Dialogue[] CurrentDialogue;

    public Vector3 OffsetPosVec;


    private void Update()
    {
        transform.position = CameraManager.Instance.MainCamera.WorldToScreenPoint(PlayerManager.Instance.playerMove.transform.position + new Vector3(OffsetPosVec.x, OffsetPosVec.y, OffsetPosVec.z));
    }

    public void Init()
    {
        CurrentDialogue = MonologueData.GetDialogoue(); //수정 예정 하나하나 start에서 호출하는게 아닌 한꺼번에 호출하는게 좋음 
        gameObject.SetActive(false);
    }


    public void ShowMonologue()
    {
        gameObject.SetActive(true);


        FunctionTimer.Create(ActiveTrue, ShowTimeSecond);

    }

    //TMP 지원은 Dotween에서 유로사용해야 DoText를 지원해주기 떄문에 임시로 만듬
    public void MonologueDoText(TextMeshProUGUI a_text, float a_duration)
    {
        a_text.maxVisibleCharacters = 0;
        DOTween.To(x => a_text.maxVisibleCharacters = (int)x, 0f, a_text.text.Length, a_duration).OnComplete(() => {
            gameObject.SetActive(false);
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        });
    }
    
    public void ActiveTrue()
    {
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = false;
        gameObject.SetActive(false);

    }

    public void SetText(string[] value) //임시 
    {
        TMPText.SetText(value[0]);
    }

    
}
