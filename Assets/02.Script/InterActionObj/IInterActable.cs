using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public interface IInteractbale 
{
  void SecondInteractOn();
  void SecondInteractOff();


  bool IsGetInterAction();
  List<GameObject> GetUIObjList();

  IEnumerator InterAct();

  Vector2 GetClimingVec();

   AnimState GetAnimState();
  
}

