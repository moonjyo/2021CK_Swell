using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractLaser : MonoBehaviour
{
    public LineRenderer Line;

    public LayerMask RefractionObjLayerMask;
    public LayerMask Stage2CrystalBallLayerMask;

    RefractLaser Refract;

    bool IsHitRefract = false;
    bool IsHitCrystalBall = false;

    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (StageManager.Instance.stage2.IsMakeStartLaser)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position + this.transform.forward * 0.25f, this.transform.right, out hit)) // 자기자신을 맞출떄가잇음
            {
                IsHitRefract = true;
               
                if ((1 << hit.transform.gameObject.layer) == Stage2CrystalBallLayerMask)
                {
                    IsHitCrystalBall = true;
                    Line.SetPosition(0, this.transform.position + this.transform.forward * 0.25f);
                    Line.SetPosition(1, hit.point);

                }
                else if((1 << hit.transform.gameObject.layer) == RefractionObjLayerMask)
                {
                    Line.SetPosition(0, this.transform.position + this.transform.forward * 0.25f);
                    Line.SetPosition(1, hit.point);
                }
                else if ((1 << hit.transform.gameObject.layer) != Stage2CrystalBallLayerMask + RefractionObjLayerMask)
                {
                    IsHitRefract = false;
                    Line.SetPosition(0, this.transform.position + this.transform.forward * 0.25f);
                    Line.SetPosition(1, this.transform.position + this.transform.right * 5);
                }
            }
            else if (!Physics.Raycast(this.transform.position, this.transform.right, out hit))
            {
                IsHitRefract = false;
                Line.SetPosition(0, this.transform.position + this.transform.forward * 0.25f);
                Line.SetPosition(1, this.transform.position + this.transform.forward * 0.25f + this.transform.right * 5);
            }
        }
    }

    public bool GetRefract(Vector3 value)
    {
        if (StageManager.Instance.stage2.SuccessMakeStartLaser())
            return false;
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + transform.forward * 0.25f, value,out hit, Mathf.Infinity, RefractionObjLayerMask))
        {
            IsHitRefract = true;
            Line.enabled = true;
            Line.SetPosition(0, this.transform.position + transform.forward * 0.25f);
            Line.SetPosition(1, hit.point);

            Refract = hit.transform.gameObject.GetComponent<RefractLaser>();
            //if(!Refract.IsHitRefract || StageManager.Instance.stage2.IsMakeStartLaser)
                Refract.GetRefract(hit.transform.right);

            return true;
        }
        else if(!StageManager.Instance.stage2.IsMakeStartLaser)
        {
            StageManager.Instance.stage2.EraseLaser(); // 간혹 버그유발?
            IsHitRefract = false;
            Line.enabled = true;
            Line.SetPosition(0, transform.position + transform.forward * 0.25f);
            //Line.SetPosition(1, transform.position + transform.forward * 5);
            Line.SetPosition(1, transform.position + transform.right * 5);
            if (Refract != null)
            Refract.Line.enabled = false;

            Refract = null;
            return false;
        }
        else
        {
            return false;
        }
    }

    public void EraseLaser()
    {
        if(Line != null && !StageManager.Instance.stage2.IsMakeStartLaser && !IsHitRefract) // 빛을 받고있지 않은애들 지우기, 라인이 존재하고있어야하고 이후엔 사라져야 할 떄
        this.Line.enabled = false;
        StageManager.Instance.stage2.Stage2Count = 0;
    }

    public void Initialize()
    {
        IsHitRefract = false;
        StageManager.Instance.stage2.Stage2Count = 0;
    }

    public bool IsHitRefractObj()
    {
        return IsHitRefract;
    }

    public bool IsHitCrystalBallObj()
    {
        return IsHitCrystalBall;
    }

}
