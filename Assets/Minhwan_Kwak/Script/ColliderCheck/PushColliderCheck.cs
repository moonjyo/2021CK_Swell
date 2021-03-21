using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushColliderCheck : MonoBehaviour
{
    public LayerMask PushLayer;

    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & PushLayer) != 0 && !PlayerManager.Instance.playerMove.IsGetItem)
        {
            PlayerManager.Instance.playerMove.isItemCol = true;
            Debug.Log(PlayerManager.Instance.playerMove.isItemCol);
            PlayerManager.Instance.playerMove.ItemColCheck(other.transform);    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & PushLayer) != 0)
        {
            PlayerManager.Instance.playerMove.isItemCol = false;
            Debug.Log(PlayerManager.Instance.playerMove.isItemCol);
            PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
            Rigidbody itemrb = other.GetComponent<Rigidbody>();
           if(itemrb != null)
            {
                itemrb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
