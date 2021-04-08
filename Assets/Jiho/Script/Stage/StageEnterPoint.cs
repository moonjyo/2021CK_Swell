using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnterPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != PlayerManager.Instance.playerMove.gameObject)
            return;

        if (GameManager.Instance.stageManager.CurrentSceneName == "Stage02")
        {
            StartCoroutine(GameManager.Instance.stageManager.EnterStage01());
        }
        else if(GameManager.Instance.stageManager.CurrentSceneName == "Stage01")
        {
            StartCoroutine(GameManager.Instance.stageManager.EnterStage02());
        }
    }
}
