using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage2 : MonoBehaviour
{
    public RefractLaser[] RefractObj;
    [HideInInspector]
    public int Stage2Count = 0;
    [HideInInspector]
    public bool IsMakeStartLaser = false;

    public GameObject CrystalballCyilnder;
    public GameObject[] Curtain;

    private void Update()
    {
        if(IsMakeStartLaser)
        {
            int i = 0;
            foreach (RefractLaser target in RefractObj)
            {
                if (target.IsHitCrystalBallObj())
                {
                    i++;
                }
                if(i >= 5)
                {
                    //스테이지클리어?
                    StartCoroutine(Stage2ClearProduction());
                    IsMakeStartLaser = false;
                    i = 0;
                }
            }
        }
      
    }

    public bool SuccessMission()
    {
        int i = 0;
        foreach (RefractLaser target in RefractObj)
        {
            if (target.IsHitRefractObj())
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
            if (!IsMakeStartLaser)
            {
                CrystalballCyilnder.transform.DOMoveY(2.7f, 3f, false);
                //StartCoroutine(Stage2ClearProduction());
            }

            IsMakeStartLaser = true;
            Stage2Count = 0;

            return true;
        }
        else
            return false;

    }

    IEnumerator Stage2ClearProduction()
    {
        //CrystalballCyilnder.transform.DOMoveY(2.7f, 3f, false);
        yield return new WaitForSeconds(3f);
        Curtain[0].transform.DOMoveX(Curtain[0].transform.position.x - 10f, 2f);
        Curtain[1].transform.DOMoveX(Curtain[1].transform.position.x + 10f, 2f);
    }

    public void EraseLaser()
    {
        foreach (RefractLaser target in RefractObj)
        {
            target.EraseLaser();
            target.Initialize();
        }
    }
}
