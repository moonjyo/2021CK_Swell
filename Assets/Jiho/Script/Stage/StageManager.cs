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

    public IEnumerator EnterStage01()
    {
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        ExitStage02();
        SceneChange("Stage01");
        stage1.SetActive(true);
        StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveIn());
        Debug.Log("move");
    }

    public IEnumerator EnterStage02()
    {
        GameManager.Instance.uiManager.UIMainMenu.Toggle(false);
        GameManager.Instance.uiManager.UIFade.Toggle(true);
        yield return StartCoroutine(GameManager.Instance.uiManager.UIFade.SceneMoveOut());
        Debug.Log("move");
        ExitStage01();
        stage2.gameObject.SetActive(true);
        SceneChange("Stage02");
 

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
