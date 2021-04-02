﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum MonsterState
{
    IDLE,
    RUN,
    JUMP,
    ATTACK,
    DIE,
};

public class MonsterBasic : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent nav;
    public Transform jumpArea;
    public Animator Anim;

    private Rigidbody RigidBody;

    MonsterState MonsterState;

    bool IsJumpArea = false; // 점프 지역에 닿았을 때
    bool IsJump = false; // 바닥 콜라이더에 닿고있을 때만?

    public LayerMask PlayerLayerMask;

    void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        MonsterState = MonsterState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        Vector3 difference = transform.position - jumpArea.position;

        if (0.5f >= difference.sqrMagnitude)
        {
            IsJumpArea = true;
            nav.enabled = false;
        }
        else if (!IsJump)
        {
            nav.enabled = true;
            IsJumpArea = false;
            
        }
    }

    private void FixedUpdate()
    {
        if (IsJumpArea)
        {
            //RigidBody.velocity += player.forward * Time.deltaTime * 5;
            RigidBody.AddForce(Vector3.Normalize(player.position - transform.position) * Time.deltaTime * 20, ForceMode.Impulse);
            IsJumpArea = false;
            if(!IsJump && MonsterState == MonsterState.IDLE)
            {
                Anim.SetTrigger("Jump"); // 여러번 들어오지 않게 시점 잡기
                Anim.speed = 0.5f;
            }
            MonsterState = MonsterState.JUMP;
            
        }
        else if (!IsJumpArea && !IsJump)
        {
            if(nav.enabled)
            {
                nav.SetDestination(player.position);
            }
            
        }
    }

    protected void Patrol()
    {
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 10.0f, PlayerLayerMask);
        int i = 0;
        while (hitCollider.Length > i)
        {
            i++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Cube" && IsJump)
        {
            IsJump = false;
            MonsterState = MonsterState.IDLE;
            Anim.speed = 1f;
            Anim.SetTrigger("Run");
        }
        // 바닥땅에 다시 닿을 때 IsJump를 false해주고 떨어질 때 true로?
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Cube" && !IsJump)
        {
            IsJump = true;
        }
    }
}