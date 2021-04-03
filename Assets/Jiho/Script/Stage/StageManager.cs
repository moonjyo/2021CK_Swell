using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public Stage2 stage2;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void EnterStage01()
    {
        SceneManager.LoadSceneAsync("Stage01", LoadSceneMode.Single);

        ExitStage2();
    }

    IEnumerator EnterStage2()
    {
        // 씬이동
        yield return SceneManager.LoadSceneAsync("Stage02", LoadSceneMode.Single);
  

        stage2.gameObject.SetActive(true);
    }

    public void ExitStage01()
    {

    }

    public void ExitStage2()
    {
        stage2.gameObject.SetActive(false);
    }
}
