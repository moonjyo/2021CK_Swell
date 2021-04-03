using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public GameObject stage1;
    public Stage2 stage2;

    public bool IsStage2Clear = false;


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        stage2 = GetComponentInChildren<Stage2>();
    }

    public IEnumerator EnterStage01()
    {
        yield return StartCoroutine(UIManager.Instance.UIMainMenu.SceneMoveOut());
        ExitStage2();
        SceneManager.LoadSceneAsync("Stage01", LoadSceneMode.Single);
        StartCoroutine(UIManager.Instance.UIMainMenu.SceneMoveIn());
    }

    public IEnumerator EnterStage02()
    {
        yield return StartCoroutine(UIManager.Instance.UIMainMenu.SceneMoveOut());
        ExitStage01();
        SceneManager.LoadSceneAsync("Stage02Test", LoadSceneMode.Single);
        StartCoroutine(UIManager.Instance.UIMainMenu.SceneMoveIn());
    }

    public void ExitStage01()
    {
        stage1.SetActive(true);
    }

    public void ExitStage2()
    {
        stage2.gameObject.SetActive(false);
    }
}
