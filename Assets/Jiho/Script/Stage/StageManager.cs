using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject stage1;
    public Stage2 stage2;

    public bool IsStage2Clear = false;

    public string CurrentSceneName;

    public Transform[] StartTr;

    public IEnumerator EnterStage01()
    {
        if (PlayerManager.Instance.PlayerInput.IsPickUpItem)
        {
            PlayerManager.Instance.PlayerInput.IsPickUpItem = false;
            StartCoroutine(PlayerManager.Instance.playerMove.InterActionItemPickDown());
        }

        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        ExitStage02();
        SceneChange("Stage01");
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerMove.IsGravity = true;
        PlayerManager.Instance.playerMove.Root_Tr.position = new Vector3(StartTr[0].position.x, StartTr[0].position.y, StartTr[0].position.z);

        stage1.SetActive(true);
        StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        Debug.Log("move");

    }

    public IEnumerator EnterStage02()
    {
        if (PlayerManager.Instance.PlayerInput.IsPickUpItem)
        {
            PlayerManager.Instance.PlayerInput.IsPickUpItem = false;
            StartCoroutine(PlayerManager.Instance.playerMove.InterActionItemPickDown());
        }
        GameManager.Instance.uiManager.UIMainMenu.Toggle(false);
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());

        ExitStage01();
        stage2.gameObject.SetActive(true);
        GameManager.Instance.stageManager.stage2.Stage2Count = 0;
        SceneChange("Stage02");

        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerMove.IsGravity = true;
        PlayerManager.Instance.playerMove.Root_Tr.position = new Vector3(StartTr[1].position.x, StartTr[1].position.y, StartTr[1].position.z);

        StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        Debug.Log("move");

    }

    public void ExitStage01()
    {
        stage1.SetActive(true);
    }

    public void ExitStage02()
    {
        GameManager.Instance.stageManager.stage2.IsMakeStartLaser = false;
        GameManager.Instance.stageManager.IsStage2Clear = false;
        GameManager.Instance.stageManager.stage2.IsInStick = false;
        stage2.gameObject.SetActive(false);
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public String CurrentGetSceneName()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        return CurrentSceneName;
    }

}
