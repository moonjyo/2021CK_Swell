using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 WalkVec;
    private Vector3 RunVec;

    public Rigidbody rb;
    public Animator PlayerAnim;
    

    private void FixedUpdate()
    {
        if(WalkVec.sqrMagnitude > 0.1f)
        {
            if (WalkVec.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {

                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            Vector3 Move = WalkVec * Time.deltaTime * PlayerManager.Instance.playerStatus.WalkSpeed;


            PlayerAnim.SetLayerWeight(1, 1);
            PlayerAnim.SetBool("Walk", true);

            rb.MovePosition(transform.position + Move);
        }
        else if (RunVec.sqrMagnitude > 0.1f)
        {

            Vector3 Move = RunVec * Time.deltaTime * PlayerManager.Instance.playerStatus.RunSpeed;

            rb.MovePosition(transform.position + Move);

            PlayerAnim.SetLayerWeight(1, 1);
            PlayerAnim.SetBool("Run", true);
        }
    }

    public void SetRunValue(Vector3 value)
    {
        RunVec = value;
    }


    public void SetWalkValue(Vector3 value)
    {
        WalkVec = value;
    }
}
