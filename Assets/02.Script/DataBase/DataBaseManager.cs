﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance;

    [SerializeField] string csv_FileName;

    public DialogueParser DialogueParser;
    public SoundData SoundData;

    public MonologueData monologueData;

    private void Awake()
    {
        SingletonInit();
    }
    private void Start()
    {
        if (monologueData != null)
        {
            Init();
        }
    }

    public void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
            if (SoundData != null)
            {
                SoundData.ReadCsv("Data/SoundText.csv");
            }
        }
    }

    public void Init()
    {
        monologueData.Init();
    }
}
