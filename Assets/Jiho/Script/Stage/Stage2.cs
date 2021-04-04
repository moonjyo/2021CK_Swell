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

    public StarStick StickInterAction;
    //[HideInInspector]
    public bool IsInStick = false; // 막대를 꽂았는지 안꽂았는지 시점

    public GameObject Stage2ToStage1EnterPoint;

    public void Start()
    {
        StartStage2();
    }


    private void Update()
    {
        if(IsMakeStartLaser && !GameManager.Instance.stageManager.IsStage2Clear)
        {
            int i = 0;
            foreach (RefractLaser target in RefractObj)
            {
                if (target.IsHitCrystalBallObj())
                {
                    i++;
                }
                if (i >= 5)
                {                 
                    StartCoroutine(Stage2ClearProduction());
                    GameManager.Instance.stageManager.IsStage2Clear = true;
                    i = 0;
                }
                else if(i < 5 && !target.IsHitRefractObj()) // 5개 모두 수정을 비추고있지 않고 비추는 오브젝트가 없을 때(별이 꺠졌을 떄) 
                {
                    SuccessMakeStartLaser();

                }
                    
            }
        }
      
    }
    public void StartStage2()
    {
        RefractObj[0] = GameObject.Find("Jewerly1").gameObject.GetComponent<RefractLaser>();
        RefractObj[1] = GameObject.Find("Jewerly2").gameObject.GetComponent<RefractLaser>();
        RefractObj[2] = GameObject.Find("Jewerly3").gameObject.GetComponent<RefractLaser>();
        RefractObj[3] = GameObject.Find("Jewerly4").gameObject.GetComponent<RefractLaser>();
        RefractObj[4] = GameObject.Find("Jewerly5").gameObject.GetComponent<RefractLaser>();

        CrystalballCyilnder = GameObject.Find("Star");
        Curtain[0] = GameObject.Find("ClockDoor1");
        Curtain[1] = GameObject.Find("ClockDoor2");

        Stage2ToStage1EnterPoint = GameObject.Find("Stage2ToStage1EnterPoint");
    }

    public bool SuccessMakeStartLaser()
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
                CrystalballCyilnder.transform.DOMoveY(0.5f, 3f, false);
                //StartCoroutine(Stage2ClearProduction());
            }

            IsMakeStartLaser = true;
            Stage2Count = 0;

            return true;
        }
        else if (IsMakeStartLaser && !IsInStick)
        {
            CrystalballCyilnder.transform.DOMoveY(-1.0f, 3f, false);
            IsMakeStartLaser = false;
            return false;
        }
        else
            return false;
            

    }

    IEnumerator Stage2ClearProduction()
    {
        //CrystalballCyilnder.transform.DOMoveY(2.7f, 3f, false);
        yield return new WaitForSeconds(3f);
        Curtain[0].transform.DOLocalMoveZ(Curtain[0].transform.localPosition.z + 0.45f, 2f);
        Curtain[1].transform.DOLocalMoveZ(Curtain[1].transform.localPosition.z - 0.45f, 2f);
        //Curtain[0].transform.DOLocalMoveZ(0.81f, 2f);
        //Curtain[1].transform.DOLocalMoveZ(-0.81f, 2f);

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
