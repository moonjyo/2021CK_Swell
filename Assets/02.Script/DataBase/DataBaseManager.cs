using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool IsFinish = false;

    public DialogueParser DialogueParser;
    public SoundData SoundData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DialougeDataInit();
            SoundData.ReadCsv("Data/SoundText.csv");
        }
    }


    public void DialougeDataInit()
    {
        Dialogue[] dialogues = DialogueParser.Parse(csv_FileName);
        for (int i = 0; i < dialogues.Length; ++i)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        IsFinish = true;

    }



    public Dialogue[] GetDialogue(int _StartNum , int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for(int i = 0; i <= _EndNum - _StartNum; ++i)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();
    }


}
