using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Stage1 stage1;
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
        StartCoroutine(ExitStage02());
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
        StartCoroutine(ExitStage01());
    }

    IEnumerator ExitStage01()
    {
        stage1.gameObject.SetActive(false);
        stage2.gameObject.SetActive(true);

        //함수로 따로뺴놓기 =================================================================================================================
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerMove.IsGravity = true;
        PlayerManager.Instance.playerMove.Root_Tr.position = new Vector3(StartTr[1].position.x, StartTr[1].position.y, StartTr[1].position.z);
        //====================================================================================================================================
        yield return StartCoroutine(SceneChange("Stage02"));
        GameManager.Instance.stageManager.stage2.StartStage2();
        StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
    }

    IEnumerator ExitStage02()
    {
        stage1.gameObject.SetActive(true);
        stage2.gameObject.SetActive(false);

        //함수로 따로뺴놓기 =================================================================================================================
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerMove.IsGravity = true;
        PlayerManager.Instance.playerMove.Root_Tr.position = new Vector3(StartTr[0].position.x, StartTr[0].position.y, StartTr[0].position.z);
        //====================================================================================================================================

        GameManager.Instance.stageManager.stage2.IsMakeStartLaser = false;
        GameManager.Instance.stageManager.IsStage2Clear = false;
        GameManager.Instance.stageManager.stage2.IsInStick = false;

        yield return StartCoroutine(SceneChange("Stage01"));



        StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());

    }

    public IEnumerator SceneChange(string sceneName)
    {

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        GameManager.Instance.stageManager.CurrentSceneName = sceneName;
    }

    public String CurrentGetSceneName()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;

        return CurrentSceneName;
    }


}
