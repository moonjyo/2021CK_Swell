using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 WalkVec;
    private Vector3 JumpVec;


    public Rigidbody rb;
    public BoxCollider GroundCheckCol;
    public LayerMask GroundLayer;


    public Animator PlayerAnim;

    public Transform Root_Tr;
    public Transform Body_Tr;

    public delegate void MoveDel();
    public MoveDel MoveFunction;

    

    private void FixedUpdate()
    {
     
        if(MoveFunction != null)
        {
            MoveFunction();
        }
        Jump();


    }


    public void SetMove(Vector3 value ,bool isRun)  // isRun = true(running) or isrun = false(walk)
    {
        WalkVec = value;
        if(!isRun)
        {
            MoveFunction = Walk;
        }
        else
        {
            MoveFunction = Run;
        }
    }
    public void SetJump(Vector3 value)
    {
        JumpVec = value;
    }


    public void Walk()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            Debug.Log("누르는중");
            //player 방향에 따라 회전 
            if (WalkVec.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {

                transform.rotation = Quaternion.Euler(0, -90, 0);
            }


            Vector3 WalkMove = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;


            PlayerAnim.SetLayerWeight(1, 1);
            PlayerAnim.SetBool("Walk", true);

            rb.MovePosition(transform.position + WalkMove);
        }
    }
    public void Run()
    {
        if (WalkVec.sqrMagnitude > 0.1f)
        {
            //player 방향에 따라 회전 
            if (WalkVec.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            { 
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }


            Vector3 WalkMove = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;


            PlayerAnim.SetLayerWeight(1, 1);
            PlayerAnim.SetBool("Run", true);

            rb.MovePosition(transform.position + WalkMove);
        }      
    }

    public void Idle()
    {
        PlayerAnim.SetLayerWeight(1, 0);
        PlayerAnim.SetLayerWeight(2, 0);
    }


    public void Jump()
    {
        if (JumpVec.sqrMagnitude > 0.1f)
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * PlayerManager.Instance.playerStatus.JumpPower);


                PlayerAnim.SetLayerWeight(2, 1);
                PlayerAnim.SetTrigger("Jump");
            }
        }
    }

    private bool IsGrounded()
    {
        bool IsCheckGround =  Physics.Raycast(GroundCheckCol.bounds.center, Vector3.down, GroundCheckCol.bounds.extents.y + 0.1f , GroundLayer);
        PlayerAnim.SetBool("Jump", false);
        return IsCheckGround;
    }


}
