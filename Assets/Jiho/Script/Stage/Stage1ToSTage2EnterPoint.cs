using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1ToSTage2EnterPoint : MonoBehaviour
{
    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        // 페이드연출 먼저 시켜줌
        // collision이 캐릭터일 때 Scene이동
        if (other.gameObject == PlayerManager.Instance.playerMove.gameObject && i == 0)
        {
            i++;
            StartCoroutine(GameManager.Instance.stageManager.EnterStage02());
        }
    }
}
