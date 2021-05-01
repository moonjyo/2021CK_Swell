using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistinguishItem : MonoBehaviour
{
    public Dictionary<string, Action> DistinguishItemDic = new Dictionary<string, Action>();

    public void init()
    {
        DistinguishItemDic.Add("MSG_Lr_ringcaseGreen_1", InteractGreenLocker);
        //DistinguishItemDic.Add()

    }

    public void InteractGreenLocker()
    {
        Debug.Log("GreenLocker");
    }
}
