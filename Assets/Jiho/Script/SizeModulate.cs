using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SizeModulate : MonoBehaviour
{
    [HideInInspector]
    public GameObject go;
    private Rigidbody goRigidBody;
    private float SetPosValue = 0; // 축소 시 오브젝트의 포지션y자리 초기화값

    private Transform PlayerPos;

    private RaycastHit hit;
    public LayerMask ItemLayerMask;

    public Vector3 SizeUpLimit = new Vector3(5.0f, 5.0f, 5.0f);
    public Vector3 SizeDownLimit = Vector3.one;

    public float RangeToItem = 3.0f; // 오브젝트와 플레이어의 거리

    float GroundColliderScaleY = 1.580888f; // 바닥의 콜라이더 스케일y값
    float GroundPosY = -0.14f; // 바닥의 포지션

    public void Awake()
    {
        PlayerPos = this.GetComponent<Transform>();
    }

    public void ItemSizeModulate(float value)
    {
        if (go == null )
        {
            return;
        }

        goRigidBody = go.GetComponent<Rigidbody>();
        SetPosValue = 0;
        if (value == 120) // 스크롤 업
        {
            if(go.transform.localScale.x >= 5.0f)
            {
                go.transform.localScale = SizeUpLimit;
                return;
            }
            else
            {
                value -= 119.9f;
                goRigidBody.mass += value; // 휠업과 사이즈업에 따른 mass값 증가
            }
        }
        else // 스크롤 다운
        {
           if (go.transform.localScale.x <= 1.0f)
            {
                go.transform.localScale = SizeDownLimit;
                return;
            }
            else
            {
                value += 119.9f;
                goRigidBody.mass += value; // 휠업과 사이즈업에 따른 mass값 감소
            }
        }
        go.transform.localScale += new Vector3(value, value, value);
        SetPosValue = (go.transform.localScale.y * 0.5f) + (GroundColliderScaleY * 0.5f) + GroundPosY;
        go.transform.position = new Vector3(go.transform.position.x, SetPosValue, go.transform.position.z);
        //스케일의 절반값이 중심일것이고 줄어들었을 때 땅에서 + 줄어든 크기의 중심길이만큼 pos.y로 고정?

    }

    public void ItemSelect(Vector2 value)
    {
        Camera mainCamera = Camera.main; // 매니저에서 main한번만 선언후 캐싱해서 가져오는게 베스트
        Ray ray = mainCamera.ScreenPointToRay(value);
        if(Physics.Raycast(ray, out hit, 1000, ItemLayerMask))
        {
            //int mask = 1 << hit.transform.gameObject.layer;
            go = hit.transform.gameObject;
            if ((PlayerPos.position - go.transform.position).magnitude > RangeToItem)
            {
                go = null;
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (go == null)
            return;
        Gizmos.color = Color.blue;
        Vector3 Direction = go.transform.position - PlayerPos.position;
        Gizmos.DrawRay(PlayerPos.position, Direction);
    }
}
