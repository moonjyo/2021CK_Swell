using UnityEngine;
using System.Collections.Generic;

public interface IInteractbale 
{
  void SecondInteractOn();
  void SecondInteractOff();


    bool IsGetInterAction();
    List<GameObject> GetUIObjList();

}

