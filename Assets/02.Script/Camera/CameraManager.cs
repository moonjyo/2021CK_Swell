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
    private void Awake()
    {
        SingletonInit();
    }

    public void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        Init();
    }


    public void Init()
    {
        StageCam.Init();
    }



}
