using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    //Dictionary<KeyCode, Action> keyDictionary;

    //void Start()
    //{
    //    keyDictionary = new Dictionary<KeyCode, Action>
    //{
    //    { KeyCode.A, KeyDown_A},
    //    { KeyCode.B, KeyDown_B},
    //    { KeyCode.C, KeyDown_C}
    //};
    //}

    //void Update()
    //{
    //    if (Input.anyKeyDown)
    //    {
    //        foreach (var dic in keyDictionary)
    //        {
    //            if (Input.GetKeyDown(dic.Key))
    //            {
    //                dic.Value();
    //            }
    //        }
    //    }
    //}

    //private void KeyDown_A()
    //{
    //    Debug.Log("A");
    //}
    //private void KeyDown_B()
    //{
    //    Debug.Log("B");
    //}
    //private void KeyDown_C()
    //{
    //    Debug.Log("C");
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Test1");
    }
}
