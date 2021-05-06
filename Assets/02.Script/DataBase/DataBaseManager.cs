using System.Collections;
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

    public void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
            SoundData.ReadCsv("Data/SoundText.csv");
        }
    }

    public void Init()
    {
        monologueData.Init();
    }





}
