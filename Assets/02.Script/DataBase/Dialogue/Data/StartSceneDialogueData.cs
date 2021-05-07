using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartSceneDialogueData : MonoBehaviour , IDialogue
{
    public string DialgoueName;

    [SerializeField]
    public string GetDialogueFileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool IsFinish;

    public StartSceneDialogueText startSceneDialogueText;

    public DialogueParser parser;
    private void Awake()
    {
        DialougeDataInit();
    }

    public void DialougeDataInit()
    {
        Dialogue[] dialogues = parser.Parse(GetDialogueFileName);

        for (int i = 0; i < dialogues.Length; ++i)
        {
            if (!dialogueDic.ContainsKey(i + 1))
            {
                dialogueDic.Add(i + 1, dialogues[i]);
            }
        }
        IsFinish = true;
    }

    public Dialogue[] GetDialogoue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i <= _EndNum - _StartNum; ++i)
        {
            dialogueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialogueList.ToArray();
    }

    public Dialogue[] GetDialogoue()
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        for (int i = 0; i < dialogueDic.Count; ++i)
        {
            dialogueList.Add(dialogueDic[i + 1]);
        }

        return dialogueList.ToArray();
    }


    public string GetDialogueName()
    {
        return DialgoueName;
    }

    public void SetDialogueName(string value)
    {
        DialgoueName = value;
    }



    public void OnSpaceBar(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
          if (startSceneDialogueText.TextCount == startSceneDialogueText.CurrentDialogue.Length) //종료
          {
              EndTalk();

              return;
          }

          startSceneDialogueText.IsNextDialogue = false;
          StartCoroutine(startSceneDialogueText.SetText()); 
        }
    }

    public void EndTalk()
    {
        LevelLoader.Instance.LoadNextLevel();
    }
}
