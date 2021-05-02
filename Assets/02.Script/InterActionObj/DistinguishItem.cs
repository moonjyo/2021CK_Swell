using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistinguishItem : MonoBehaviour
{
    public Dictionary<string, Action> DistinguishItemDic = new Dictionary<string, Action>();

    public void init()
    {
        DistinguishItemDic.Add("MSG_Lr_lokerGreen_1", InteractGreenLocker); // 상호작용이 되는 오브젝트 이름을 키로잡음
        //DistinguishItemDic.Add()

    }

    public void InteractGreenLocker()
    {
        Debug.Log("GreenLocker Open");
    }
}
