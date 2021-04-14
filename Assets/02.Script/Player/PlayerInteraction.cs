using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask WallLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer & WallLayer) != 0)
        {
            PlayerManager.Instance.playerStatus.FsmAdd(PlayerFSM.Wall);
        }
    }
    private void OnCollisionExit(Collision collision)
    { 
        if ((1 << collision.gameObject.layer & WallLayer) != 0)
        {
            PlayerManager.Instance.playerStatus.FsmRemove(PlayerFSM.Wall);
        }
    }
}
