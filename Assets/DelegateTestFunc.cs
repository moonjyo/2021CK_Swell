using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelegateTestFunc : MonoBehaviour
{
  public  Dictionary<string, Action> ActionDicFunc;
    // Start is called before the first frame update
   
    public void Init()
    {
        ActionDicFunc.Add("Fire1", FIre1);
        ActionDicFunc.Add("Fire2", Fire2);

    }





    public void FIre1()
    {

    }

    public void Fire2()
    {

    }
}
    