using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField]
    DialogueEvent dialouge;

    public Dialogue[] GetDialogue()
    {
        dialouge.dialogues = DataBaseManager.Instance.GetDialogue((int)dialouge.line.x ,(int)dialouge.line.y);
        return dialouge.dialogues;
    }

    private void Start()
    {
        GetDialogue();
    }
}
