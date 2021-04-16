using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer
{

    private static List<FunctionTimer> activeTimeList;
    private static GameObject initGameObject;
    private static void InitIfNeeded()
    {
        if(initGameObject == null)
        {
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            activeTimeList = new List<FunctionTimer>();
        }
    }

    public class MonoBehavidourHook : MonoBehaviour
    {
        public Action onUpdate;
        private void Update()
        {
            if (onUpdate != null) onUpdate();
        }
    }

     public static void StopTimer(string timername)
     {
        for(int i = 0; i <  activeTimeList.Count; ++i)
        {
            if(activeTimeList[i].timername  == timername)
            {
                activeTimeList[i].DestroySelf();
                i--;
            }
        }
    }

    public static FunctionTimer Create(Action  action , float timer, string timerName = null)
    {
        InitIfNeeded();
        bool IsSame =  SameNameStopTimer(timerName);
        if (IsSame)
        {
            return null;
        }
            GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehavidourHook));

            FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);

            gameObject.GetComponent<MonoBehavidourHook>().onUpdate = functionTimer.Update;

            activeTimeList.Add(functionTimer);
            return functionTimer;
        
    }

    public static bool SameNameStopTimer(string CreateTimerName)
    {
        for(int i =0; i < activeTimeList.Count; ++i)
        {
            if(activeTimeList[i].timername == CreateTimerName)
            {
                return true;
            }
        }
        return false;
    }

    private static void RemoveTimer(FunctionTimer functionTimer)
    {
        InitIfNeeded();
        activeTimeList.Remove(functionTimer);
    }
    private Action action;
    private float timer;
    private bool isDestroyed;
    private GameObject gameObject;
    private string timername;
    public FunctionTimer(Action action , float timer ,string timername,  GameObject gameObject)
    {
        this.action = action;
        this.timer = timer;
        this.timername = timername;
        this.gameObject = gameObject;
        this.isDestroyed = false;
    }


    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                DestroySelf();
                //Trigger Action 
            }
        }
    }


    private void DestroySelf()
    {
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }
}
