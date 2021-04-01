using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;
    public EventInstance bgm;

    [EventRef]
    public string[] BgmClip;

    private Bus MasterBus;
    private Bus BgmBus;
    private Bus sfxBus;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }


        MasterBus = RuntimeManager.GetBus("bus:/Master");
        BgmBus = RuntimeManager.GetBus("bus:/Master/BGM");
        sfxBus = RuntimeManager.GetBus("bus:/Master/SFX");
    }

    private void Start()
    {
     //   Change(0);
     //   Play();
    }

    public void Change(int index)
    {
        notifyRelease();

        bgm = RuntimeManager.CreateInstance(BgmClip[index]);
        
    }
    public void notifyRelease()
    {
        if (bgm.isValid())
        {
            Stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgm.release();
            bgm.clearHandle();
        }
    }
    public void PlayOneShot(string path)
    {
        //효과음을 재생할 인스턴스를 생성합니다.
        var sfx = RuntimeManager.CreateInstance(path);

        //재생
        sfx.start();

        //릴리즈
        sfx.release();
    }

    public void Stop(FMOD.Studio.STOP_MODE SM) => bgm.stop(SM);
    public void Play() => bgm.start();
    
    public void setPause(bool pause) => bgm.setPaused(pause);
    public void setMasterVolume(float Value) => MasterBus.setVolume(Value);

    public void setBGMVolume(float Value) => BgmBus.setVolume(Value);
    public void setSFXVolume(float Value) => sfxBus.setVolume(Value);




}
