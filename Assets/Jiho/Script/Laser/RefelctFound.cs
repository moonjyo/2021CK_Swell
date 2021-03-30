using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefelctFound : MonoBehaviour
{
    public Transform StartToRaser;
    public Transform Target;

    public LayerMask ReflectObject; // 반사물체
    public LayerMask ConcaveLensLayerMask; // 오목렌즈
    public LayerMask ConvexLensLayerMask; // 볼록렌즈
    public LayerMask JewerlyLayerMask; // 보석

    int CheckLayerMask;

    LineRenderer Line; // 쏘는 레이저

    int ReflectCount = 1;

    Vector3 LaserForward;

    bool IsTouchLens = false;

    LensLight LensLight;
    RefractLaser Refract;

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
        // transform.position이 바뀌면 지우고 새로그려야함
        //if (OriginLaserPos != transform.position)
        //{
        //    OriginLaserPos = transform.position;
        //    ShootLaser(transform.position, LaserForward);
        //}
        LaserForward = transform.forward;
        ShootLaser(transform.position, LaserForward);
        if(StageManager.Instance.Stage2Clear)
        {
            //보석의 빛을 수정구에 집중시킨다.
            //커튼이 제쳐지며 물고기 상패가 드러난다.
        }
    }

    public void ShootLaser(Vector3 StartPos, Vector3 value)
    {
        RaycastHit hit;
        if (Physics.Raycast(StartPos, value, out hit, Mathf.Infinity, CheckLayerMask))
        {
            if ((1 << hit.transform.gameObject.layer) == ConcaveLensLayerMask) // 오목렌즈에 히트됐을 때
            {
                // -hit.transform.forward 쪽으로 빛이 번지게
                // 새로운 Linerenderer 생성?
                Debug.Log("오목렌즈");
                IsTouchLens = true;
                Line.SetPosition(ReflectCount, hit.point);
                if (ReflectCount < Line.positionCount - 1)
                {
                    Line.SetPosition(ReflectCount + 1, hit.point);
                }
                // 함수실행
                //LensLight.GetConcaveLens(value, hit.point);
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
                //hit.collider.gameObject.GetComponent<LensLight>().Line.enabled = false;
                if (IsTouchLens)
                    LensLight.Line.enabled = false;
                IsTouchLens = false;
               
            }
           
            Vector3 normalVector = hit.transform.forward;
            Vector3 reflectVector = Vector3.Reflect(value, normalVector);

            reflectVector = reflectVector.normalized;

            Line.enabled = true;
            switch (ReflectCount)
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
                //Line.SetPosition(2, hit.transform.position);
                //Line.SetPosition(3, hit.transform.position);
            }
            else
            {
                //Line.SetPosition(2, hit.transform.position + hit.transform.forward * 5);
                //Line.SetPosition(3, hit.transform.position + hit.transform.forward * 5);
                Line.SetPosition(2, hit.transform.position);
                Line.SetPosition(3, hit.transform.position);
            }
        }
        else if (!Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, CheckLayerMask))
        {
            Line.enabled = false;
            IsTouchLens = false;
            if (LensLight != null)
                LensLight.Line.enabled = false;
            if (Refract != null && !StageManager.Instance.Stage2Clear)
                StageManager.Instance.EraseLaser();
        }
     
    }

}
