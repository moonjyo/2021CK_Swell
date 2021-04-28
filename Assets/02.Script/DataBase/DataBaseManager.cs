using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance;

    [SerializeField] string csv_FileName;

    public DialogueParser DialogueParser;
    public SoundData SoundData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            SoundData.ReadCsv("Data/SoundText.csv");
        }
    }





}
