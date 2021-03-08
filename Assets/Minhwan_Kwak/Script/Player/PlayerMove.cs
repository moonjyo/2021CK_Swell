using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 WalkVec;

    public Rigidbody rb;
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


    public void Walk()
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
    }


}
