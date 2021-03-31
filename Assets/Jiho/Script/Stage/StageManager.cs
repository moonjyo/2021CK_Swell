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
    }
}
