using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !TriggerManager.Instance.IsCutScene)
        {
            TriggerManager.Instance.IsCutScene = true;
            LevelLoader.Instance.LoadNextLevel((int)LoadSceneIndex.Cutscene1);

        }
    }
}
