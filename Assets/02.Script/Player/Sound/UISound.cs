using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISound : MonoBehaviour
{
    public Slider SfxSlider;
    public Slider BgmSlider;



    public void Init()
    {
        SfxSlider.value = DataBaseManager.Instance.SoundData.SfxData;
        BgmSlider.value = DataBaseManager.Instance.SoundData.BgmData;

        AudioManager.Instance.setSFXVolume(SfxSlider.value);
        AudioManager.Instance.setBGMVolume(BgmSlider.value);


    }



    public void SfxSoundModule()
    {
        AudioManager.Instance.setSFXVolume(SfxSlider.value);
        DataBaseManager.Instance.SoundData.SaveSfxData(SfxSlider.value);

    }
    public void BgmSoundModule()
    {
        AudioManager.Instance.setBGMVolume(BgmSlider.value);
        DataBaseManager.Instance.SoundData.SaveBgmData(BgmSlider.value);
    }

    private void OnApplicationQuit()
    {
        DataBaseManager.Instance.SoundData.WriteData();
    }

}
