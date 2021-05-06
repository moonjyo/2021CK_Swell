using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour
{
    public  StageCamera StageCam;
    public  Camera MainCamera;
    public Camera CaptureCamera;
    public Camera ObserveCamera;
    public static CameraManager Instance;


    public void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }


    public void Init()
    {
        StageCam.Init();
    }



}
