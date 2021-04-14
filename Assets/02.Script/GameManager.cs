﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public StageManager stageManager;
    public UIManager uiManager;
  
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GameStart()
    {
        uiManager.UIMainMenu.Toggle(true);
        uiManager.UISettingOptionMenu.Toggle(false);
        uiManager.UIFade.Toggle(false);
    }


}