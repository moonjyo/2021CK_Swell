using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void Start()
    {
        FunctionTimer.Create(TestingAction, 3f , "TestringTimer1");

        FunctionTimer.Create(TestingAction_2, 4f , "TestingTImer2");

        FunctionTimer.StopTimer("TestingTImer2");
    }

    private void Update()
    { 
    }

    private void TestingAction()
    {
        Debug.Log("Testing!");
    }
    private void TestingAction_2()
    {
        Debug.Log("Testing!");
    }
}
