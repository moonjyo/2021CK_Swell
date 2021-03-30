using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public RefractLaser[] RefractObj;
    public int Stage2Count = 0;
    public bool Stage2Clear = false;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    public bool SuccessMission()
    {
        int i = 0;
        foreach (RefractLaser target in RefractObj)
        {
            if(target.IsHitRefractObj())
            {
                i++;
            }
        }

        if (i >= 5) // 5개 빛을 모두 보석에 모았을 때
        {
            foreach (RefractLaser target in RefractObj)
            {
                target.Line.enabled = true;
            }
            Stage2Clear = true;
            Stage2Count = 0;
            return true;
        }
        else
            return false;
        
    }

    public void EraseLaser()
    {
        foreach(RefractLaser target in RefractObj)
        {
            target.EraseLaser();
            target.Initialize();
        }
    }
}
