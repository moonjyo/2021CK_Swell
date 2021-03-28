using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool IsFinish = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DialogueParser therParser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = therParser.Parse(csv_FileName);
            for(int i = 0; i < dialogues.Length; ++i)
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
            IsFinish = true;    
        }
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
