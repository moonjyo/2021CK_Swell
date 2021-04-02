using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefelctFound : MonoBehaviour
{
    public GameObject Flash;

    public Transform StartToRaser;
    public Transform Target;

    public LayerMask ReflectObject; // 반사물체
    public LayerMask ConcaveLensLayerMask; // 오목렌즈
    public LayerMask ConvexLensLayerMask; // 볼록렌즈
    public LayerMask JewerlyLayerMask; // 보석

    public int LaserAdvanceLength = 7;

    int CheckLayerMask;

    LineRenderer Line; // 쏘는 레이저

    int ReflectCount = 1;

    Vector3 LaserForward;

    bool IsTouchLens = false;

    LensLight LensLight;
    RefractLaser Refract;

    private bool IsToggle = false;

    private Vector2 AngleVec;
    public Vector2 AngleMinMax;
    public float AngleSpeed = 0f;


    void Start()
    {
        Line = GetComponent<LineRenderer>();
        LaserForward = transform.forward;
        Line.positionCount = 4; // 유동적으로?
        CheckLayerMask = ReflectObject + ConcaveLensLayerMask + ConvexLensLayerMask;
        //CheckLayerMask = (1 << LayerMask.NameToLayer("Item")) + (1 << LayerMask.NameToLayer("ConcaveLens") + (1 << LayerMask.NameToLayer("ConvexLensLayerMask")));
    }

    void Update()
    {
        SetAngleValue();
        LaserForward = transform.forward;
        ShootLaser(transform.position, LaserForward);
    }
    public void SetAngleValue(Vector2 value)
    {
        AngleVec = value;
    }

    public void ShootLaser(Vector3 StartPos, Vector3 value)
    {
        RaycastHit hit;
        if (Physics.Raycast(StartPos, value, out hit, Mathf.Infinity, CheckLayerMask))
        {
            if ((1 << hit.transform.gameObject.layer) == ConcaveLensLayerMask) // 오목렌즈에 히트됐을 때
            {
                IsTouchLens = true;
                Line.SetPosition(ReflectCount, hit.point);
                if (ReflectCount < Line.positionCount - 1)
                {
                    Line.SetPosition(ReflectCount + 1, hit.point);
                    if(ReflectCount < Line.positionCount - 2)
                    Line.SetPosition(ReflectCount + 2, hit.point);
                }
                LensLight = hit.collider.gameObject.GetComponent<LensLight>();
                LensLight.GetConcaveLens(value, hit.point);
            }
            else if ((1 << hit.transform.gameObject.layer) == ConvexLensLayerMask)
            {
                Debug.Log("볼록렌즈");
                IsTouchLens = true;
                Line.SetPosition(ReflectCount, hit.point);
                if (ReflectCount < Line.positionCount - 1)
                {
                    Line.SetPosition(ReflectCount + 1, hit.point);
                }
            }
            else
            {
                if (IsTouchLens)
                    LensLight.Line.enabled = false;
                IsTouchLens = false;
               
            }
           
            Vector3 normalVector = hit.transform.forward;
            Vector3 reflectVector = Vector3.Reflect(value, normalVector);

            reflectVector = reflectVector.normalized;

            Line.enabled = true;
            switch (ReflectCount) // 반사 횟수에 따라서 새로짜야함
            {
                case 1:
                    Line.SetPosition(0, StartPos);
                    Line.SetPosition(1, hit.point);
                    break;
                case 2:
                    Line.SetPosition(2, hit.point);
                    break;
                case 3:
                    Line.SetPosition(3, hit.point);
                    break;
            }

            if (ReflectCount < 3 && !IsTouchLens) // 반사횟수제한 2회
            {
                ReflectCount++;
                ShootLaser(hit.point, reflectVector);
            }
            else // 2회이상이 되면 되돌아감
            {
                ReflectCount = 1;
                return;
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, JewerlyLayerMask))
        {
            Line.enabled = true;
            IsTouchLens = false;
            if (LensLight != null)
                LensLight.Line.enabled = false;

            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, hit.point);

            Refract = hit.transform.GetComponent<RefractLaser>();

            if(Refract.GetRefract(hit.transform.forward))
            {
                Line.SetPosition(2, hit.point);
                Line.SetPosition(3, hit.point);
            }
            else
            {
                Line.SetPosition(2, hit.transform.position);
                Line.SetPosition(3, hit.transform.position);
            }
        }
        else if (!Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, CheckLayerMask))
        {
            //Line.enabled = false;
            IsTouchLens = false;
            if (LensLight != null)
                LensLight.Line.enabled = false;
            if (Refract != null && !StageManager.Instance.stage2.IsMakeStartLaser)
                StageManager.Instance.stage2.EraseLaser();

            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, transform.position + transform.forward * LaserAdvanceLength);
            Line.SetPosition(2, transform.position + transform.forward * LaserAdvanceLength);
            Line.SetPosition(3, transform.position + transform.forward * LaserAdvanceLength);
        }
        else if(ReflectCount == 3)
        {
            Line.SetPosition(3, StartPos + value * LaserAdvanceLength);
            ReflectCount = 1;
        }
            

    }
    public void Toggle()
    {
        IsToggle = !IsToggle;
        if (IsToggle)
        {
            FlashOn();
        }
        else
        {
            FlashOff();
        }
    }


    public void FlashOff()
    {
        Flash.SetActive(false);
    }
    public void FlashOn()
    {
        Flash.SetActive(true);
    }

    public void SetAngleValue()
    {
        if (AngleVec.sqrMagnitude > 0.1f)
        {//down
            if (AngleVec.y == 1)
            {
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z) * Time.deltaTime * AngleSpeed);

                if (transform.eulerAngles.z >= AngleMinMax.x)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, AngleMinMax.x);
                }

            }
            else
            {
                //up
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z) * Time.deltaTime * -AngleSpeed);

                if (transform.eulerAngles.z <= AngleMinMax.y)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, AngleMinMax.y);
                }
            }

        }
    }

}
