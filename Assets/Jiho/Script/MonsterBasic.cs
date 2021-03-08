using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBasic : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent nav;
    public Transform jumpArea;

    bool IsJumpArea = false; // 점프 지역에 닿았을 때
    bool IsJump = false; // 바닥 콜라이더에 닿고있을 때만?

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 일정 범위를 놓고 레이캐스트를 쏴서 장애물이 아니고 플레이어면 접근 및 공격?
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 10.0f);
        int i = 0;
        while (hitCollider.Length > i)
        {
            i++;
        }
        Vector3 difference = transform.position - jumpArea.position;

        if (1 > difference.magnitude)
        {
            IsJumpArea = true;
            nav.enabled = false;
        }
        else if (!IsJump)
        {
            IsJumpArea = false;
            nav.enabled = true;
        }

        if (IsJumpArea)
        {
            transform.Translate(player.forward * Time.deltaTime * 5);
            IsJump = true;
        }
        else if (!IsJumpArea)
        {
            nav.SetDestination(player.position);
        }





    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥땅에 다시 닿을 때 IsJump를 false해주고 떨어질 때 true로?
    }
}
