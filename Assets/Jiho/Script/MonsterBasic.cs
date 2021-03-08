using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBasic : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 일정 범위를 놓고 레이캐스트를 쏴서 장애물이 아니고 플레이어면 접근 및 공격?
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 10.0f);
        int i = 0;
        while(hitCollider.Length > i)
        {
            i++;
        }
    }
}
