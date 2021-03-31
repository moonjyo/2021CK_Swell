using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushColliderCheck : MonoBehaviour
{
    public LayerMask PushLayer;
    public LayerMask ItemLayer;


    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & PushLayer) != 0)
        {
            PlayerManager.Instance.playerMove.isItemCol = true;
            PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.ItemTouch);

          Rigidbody InterActionrb  =  other.GetComponent<Rigidbody>();
            if (InterActionrb != null)
            {
                PlayerManager.Instance.playerMove.SetInterActionObj(InterActionrb);
            }
        }
        if ((1 << other.gameObject.layer & ItemLayer) != 0)
        {
            PlayerManager.Instance.playerMove.isItemCol = true;

            Rigidbody InterActionrb = other.GetComponent<Rigidbody>();
            if (InterActionrb != null)
            {
                PlayerManager.Instance.playerMove.SetGetItemObj(InterActionrb);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & PushLayer) != 0)
        {
            PlayerManager.Instance.playerMove.isItemCol = false;
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.ItemTouch);
            PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        }
        if ((1 << other.gameObject.layer & ItemLayer) != 0)
        {
            PlayerManager.Instance.playerMove.isItemCol = false;
            PlayerManager.Instance.playerMove.SetRemoveGetItemObj();
        }
    }
}
