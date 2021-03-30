using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractLaser : MonoBehaviour
{
    public LineRenderer Line;

    public LayerMask RefractionObjLayerMask;

    RefractLaser Refract;

    bool IsHitRefract = false;

    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
    }

    public bool GetRefract(Vector3 value)
    {
        if (StageManager.Instance.SuccessMission())
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
        else if(!StageManager.Instance.Stage2Clear)// 가리키지 않을 때 그 뒤에 연결되었던 보석들의 빛 제거해야함
        {
            StageManager.Instance.EraseLaser();
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
        if(Line != null && !StageManager.Instance.Stage2Clear)
        this.Line.enabled = false;
        StageManager.Instance.Stage2Count = 0;
    }

    public void Initialize()
    {
        IsHitRefract = false;
        StageManager.Instance.Stage2Count = 0;
    }

    public bool IsHitRefractObj()
    {
        return IsHitRefract;
    }

}
