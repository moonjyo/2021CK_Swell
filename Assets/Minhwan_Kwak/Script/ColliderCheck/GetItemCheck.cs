using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemCheck : MonoBehaviour
{
    public LayerMask ItemLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer & ItemLayer) != 0) && !PlayerManager.Instance.PlayerInput.IsPickUpItem)
        {
            PlayerManager.Instance.playerMove.IsItemCol = true;

            InterActionObjBase InterActionrb = other.GetComponent<InterActionObjBase>();
            if (InterActionrb != null)
            {
                PlayerManager.Instance.playerMove.SetGetItemObj(InterActionrb);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & ItemLayer) != 0)
        {
            PlayerManager.Instance.playerMove.IsItemCol = false;
        }
    }


}
