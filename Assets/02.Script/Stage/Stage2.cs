using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage2 : MonoBehaviour
{
    [HideInInspector]
    public RefractLaser[] RefractObj;
    [HideInInspector]
    public bool IsMakeStarLaser = false;

    [HideInInspector]
    public GameObject CrystalballCyilnder;
    [HideInInspector]
    public GameObject[] Curtain;

    public StarStick StickInterAction;
    //[HideInInspector]
    public bool IsInStick = false; // 막대를 꽂았는지 안꽂았는지 시점

    public GameObject Stage2ToStage1EnterPoint;

    public RefractLaser OriginShootLaser;
    public List<RefractLaser> HitRefractObj = new List<RefractLaser>();

    [HideInInspector]
    public GameObject GarbageLocationObj;

    private void Update()
    {
        if(IsMakeStarLaser && !GameManager.Instance.stageManager.IsStage2Clear)
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

        RefractObj[0].ForMakeStarRefractObj = RefractObj[2];
        RefractObj[1].ForMakeStarRefractObj = RefractObj[3];
        RefractObj[2].ForMakeStarRefractObj = RefractObj[4];
        RefractObj[3].ForMakeStarRefractObj = RefractObj[0];
        RefractObj[4].ForMakeStarRefractObj = RefractObj[1];

        CrystalballCyilnder = GameObject.Find("Star");
        Curtain[0] = GameObject.Find("ClockDoor1");
        Curtain[1] = GameObject.Find("ClockDoor2");

        Stage2ToStage1EnterPoint = GameObject.Find("Stage2ToStage1EnterPoint");
        StickInterAction = CrystalballCyilnder.GetComponentInChildren<StarStick>();

        GameManager.Instance.stageManager.stage2.IsMakeStarLaser = false;
        GameManager.Instance.stageManager.IsStage2Clear = false;
        GameManager.Instance.stageManager.stage2.IsInStick = false;

        foreach (RefractLaser atarget in GameManager.Instance.stageManager.stage2.RefractObj)
        {
            atarget.IsHitCrystalBall = false;
            if(atarget.Line != null)
            {
                atarget.Line.enabled = false;
            }
            
        }
      
    }

    public bool SuccessMakeStartLaser() 
    {
        int i = 0;

        foreach(RefractLaser target in RefractObj)
        {
            if(target.ForMakeStarRefractObj == target.GetRefractObj())
            {
                i++;
            }
        }

        if (i >= 5) // 5개 빛이 모두 별모양을 그렸을 때, 보석들이 비추어야 할 다른 보석들을 비추고 있을 때
        {
            foreach (RefractLaser target in RefractObj)
            {
                target.Line.enabled = true;
            }
            if (!IsMakeStarLaser)
            {
                CrystalballCyilnder.transform.DOMoveY(0.5f, 0.5f, false);
                IsMakeStarLaser = true;
            }

            return true;
        }
        else if (IsMakeStarLaser && !IsInStick)
        {
            CrystalballCyilnder.transform.DOMoveY(-1.0f, 0.5f, false);
            IsMakeStarLaser = false;
            return false;
        }
        else
        {
            return false;
        }


    }

    IEnumerator Stage2ClearProduction()
    {
        yield return new WaitForSeconds(3f);
        Curtain[0].transform.DOLocalMoveZ(Curtain[0].transform.localPosition.z + 0.45f, 2f);
        Curtain[1].transform.DOLocalMoveZ(Curtain[1].transform.localPosition.z - 0.45f, 2f);


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
