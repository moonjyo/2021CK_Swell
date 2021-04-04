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

    public IEnumerator EnterStage01()
    {
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        ExitStage02();
        stage1.SetActive(true);
        yield return StartCoroutine(SceneChange("Stage01"));
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        GameManager.Instance.uiManager.UIFade.Toggle(false);
        Debug.Log("stage1 move");
    }

    public IEnumerator EnterStage02()
    {
        GameManager.Instance.uiManager.UIMainMenu.Toggle(false);
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        ExitStage01();
        stage2.gameObject.SetActive(true);
        yield return StartCoroutine(SceneChange("Stage02"));
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        Debug.Log("stage2 move");
        GameManager.Instance.stageManager.stage2.StartStage2();
        GameManager.Instance.uiManager.UIFade.Toggle(false);


    }

    public void ExitStage01()
    {
        stage1.SetActive(true);
    }

    public void ExitStage02()
    {
        stage2.gameObject.SetActive(false);
    }

    IEnumerator SceneChange(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return null;
    }
}
