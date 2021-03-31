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
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, Mathf.Infinity))
            {
                if ((1 << hit.transform.gameObject.layer) == Stage2CrystalBallLayerMask)
                {
                    IsHitCrystalBall = true;
                }
                Line.SetPosition(0, this.transform.position);
                Line.SetPosition(1, hit.point);
            }
            else
            {
                Line.SetPosition(0, this.transform.position);
                Line.SetPosition(1, this.transform.position + this.transform.forward * 5);
            }

        }

    }

    public bool GetRefract(Vector3 value)
    {
        if (StageManager.Instance.stage2.SuccessMission())
            return false;
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, value,out hit, Mathf.Infinity, RefractionObjLayerMask))
        {
            IsHitRefract = true;
            Line.enabled = true;
            Line.SetPosition(0, this.transform.position);
            Line.SetPosition(1, hit.point);

            Refract = hit.transform.gameObject.GetComponent<RefractLaser>();
            if(!Refract.IsHitRefract)
            Refract.GetRefract(hit.transform.forward);

            return true;
        }
        else if(!StageManager.Instance.stage2.IsMakeStartLaser)// 가리키지 않을 때 그 뒤에 연결되었던 보석들의 빛 제거해야함
        {
            StageManager.Instance.stage2.EraseLaser(); // 수정 필요할 거같음 간혹 버그유발
            IsHitRefract = false;
            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, transform.position + transform.forward * 5);
            if(Refract != null)
            Refract.Line.enabled = false;

            return false;
        }
        else
        {
            return false;
        }
    }

    public void EraseLaser()
    {
        if(Line != null && !StageManager.Instance.stage2.IsMakeStartLaser)
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
