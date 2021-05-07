using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IDialogueText
{
     void Init();
     void ShowDialogue();
     IEnumerator SetText();
     void DialogueDoText(TextMeshProUGUI a_text, float a_duration);
}
