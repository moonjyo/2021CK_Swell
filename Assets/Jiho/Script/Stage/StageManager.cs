using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    public void EnterStage2()
    {
        stage2.gameObject.SetActive(true);
    }

    public void ExitStage2()
    {
        stage2.gameObject.SetActive(false);
    }
}
