using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractLaser : MonoBehaviour
{
    public LineRenderer Line;

    public LayerMask RefractionObjLayerMask;
    public LayerMask Stage2CrystalBallLayerMask;
    public LayerMask PlayerLayerMask;
    public LayerMask ItemLayerMask;

    RefractLaser Refract;
    public RefractLaser ForMakeStarRefractObj;

    bool IsHitRefract = false;
    public bool IsHitCrystalBall = false;

    Vector3 RaserStartPoint;

    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (PlayerManager.Instance.flashLight.gameObject.activeInHierarchy == false && !GameManager.Instance.stageManager.stage2.IsMakeStarLaser)
        {
            Line.enabled = false;
            //GameManager.Instance.stageManager.stage2.SuccessMakeStartLaser();
            return;
        }

        if (GameManager.Instance.stageManager.stage2.IsMakeStarLaser)
        {
            RaserStartPoint = this.transform.position + this.transform.forward * 0.25f;
            RaycastHit hit;
            if (Physics.Raycast(RaserStartPoint, this.transform.right, out hit)) // 자기자신을 맞출떄가잇음
            {
                IsHitRefract = true;
               
                if ((1 << hit.transform.gameObject.layer) == Stage2CrystalBallLayerMask)
                {
                    IsHitCrystalBall = true;
                    Line.SetPosition(0, RaserStartPoint);
                    Line.SetPosition(1, hit.point);

                }
                else if((1 << hit.transform.gameObject.layer) == RefractionObjLayerMask)
                {
                    IsHitCrystalBall = false;
                    Line.SetPosition(0, RaserStartPoint);
                    Line.SetPosition(1, hit.point);
                }

                else if ((1 << hit.transform.gameObject.layer) == PlayerLayerMask || (1 << hit.transform.gameObject.layer) == ItemLayerMask) // 캐릭터 일때 꺼지더라도 지나가고나면 빛은 유지되게
                {

                    Line.SetPosition(0, RaserStartPoint);
                    Line.SetPosition(1, hit.point);
                    Refract = null; // 유지할지말지 고민
                }
                else
                {
                    Line.SetPosition(0, RaserStartPoint);
                    Line.SetPosition(1, RaserStartPoint + this.transform.right * 15);
                    IsHitCrystalBall = false;
                    IsHitRefract = false;
                    Refract = null;
                    
                }
            }
            else if (!Physics.Raycast(this.transform.position, this.transform.right, out hit))
            {
                IsHitRefract = false;
                Line.SetPosition(0, RaserStartPoint);
                Line.SetPosition(1, RaserStartPoint + this.transform.right * 15);
            }
        }
    }

    public bool GetRefract(Vector3 value)
    {
        if (GameManager.Instance.stageManager.stage2.SuccessMakeStartLaser())
            return false;
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + transform.forward * 0.25f, value,out hit, Mathf.Infinity, RefractionObjLayerMask))
        {
            IsHitRefract = true;
            Line.enabled = true;
            Line.SetPosition(0, this.transform.position + transform.forward * 0.25f);
            Line.SetPosition(1, hit.point);

            Refract = hit.transform.gameObject.GetComponent<RefractLaser>();

            foreach(RefractLaser target in GameManager.Instance.stageManager.stage2.HitRefractObj)
            {
                if(target == Refract)
                {
                    //Refract = null;
                    GameManager.Instance.stageManager.stage2.HitRefractObj.Clear();
                    return true;
                }
            }

            GameManager.Instance.stageManager.stage2.HitRefractObj.Add(Refract);

            Refract.GetRefract(hit.transform.right);
          

            return true;
        }
        else if(!GameManager.Instance.stageManager.stage2.IsMakeStarLaser)
        {
            GameManager.Instance.stageManager.stage2.EraseLaser(); // 간혹 버그유발?

            if(GameManager.Instance.stageManager.stage2.HitRefractObj.Contains(GameManager.Instance.stageManager.stage2.OriginShootLaser))
            {
                GameManager.Instance.stageManager.stage2.HitRefractObj.Clear();
            }

            IsHitRefract = false;
            Line.enabled = true;
            Line.SetPosition(0, this.transform.position + this.transform.forward * 0.25f);
            //Line.SetPosition(1, transform.position + transform.forward * 5);
            Line.SetPosition(1, transform.position + this.transform.forward * 0.25f + transform.right * 15);
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
        if(Line != null && !GameManager.Instance.stageManager.stage2.IsMakeStarLaser && !IsHitRefract) // 빛을 받고있지 않은애들 지우기, 라인이 존재하고있어야하고 이후엔 사라져야 할 떄
        this.Line.enabled = false;
    }

    public void Initialize()
    {
        IsHitRefract = false;
    }

    public bool IsHitRefractObj()
    {
        return IsHitRefract;
    }

    public bool IsHitCrystalBallObj()
    {
        return IsHitCrystalBall;
        
    }

    public void SetIsHitCrystalBallObj(bool setvalue)
    {
        IsHitCrystalBall = setvalue;
    }

    public RefractLaser GetRefractObj()
    {
        return Refract;
    }

}
