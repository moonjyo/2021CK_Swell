using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushColliderCheck : MonoBehaviour
{
    public LayerMask PushLayer;

    private void OnTriggerEnter(Collider other)
    {
         if((1 << other.gameObject.layer & PushLayer) != 0 && PlayerManager.Instance.playerMove.PushItemCheck())
        {
            PlayerManager.Instance.playerMove.IsInterActionCol = true;
            PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.ItemTouch);

          Rigidbody InterActionrb  =  other.GetComponent<Rigidbody>();
            if (InterActionrb != null)
            {
                PlayerManager.Instance.playerMove.SetInterActionObj(InterActionrb);
                Debug.Log("충돌중");
            }
        }
    }

}
