using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{
   void DialougeDataInit();
    Dialogue[] GetDialogoue(int _strName, int _EndName);
    Dialogue[] GetDialogoue();
    string GetDialogueName();
   void SetDialogueName(string value);

  


}

