using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDown : MonoBehaviour , IInteractbale
{
    public Animator LeverDownAnim;
    public GameObject BaseMentObj;
    public Animator BaseMentAnim;
    public BoxCollider col;
    public BoxCollider LightCol;

    public LayerMask PlayerLayer;

    private bool IsPlay = false;

    public Transform PlayerTargetTransform;
    
    public Vector3 OffsetPos;


    public void Interact()
    {
        Debug.Log("Lever");
       // StartCoroutine(InterActionLever());
    }

    public IEnumerator InterActionLever()
    {
        while (!IsPlay)
        {
            if (PlayerManager.Instance.playerAnimationEvents.PlayerAnim)
            {
                PlayerManager.Instance.playerStatus.FsmAllRemove();
                PlayerManager.Instance.playerMove.Root_Tr.localPosition = OffsetPos;
                yield return new WaitForSeconds(0.4f);
                BaseMentObj.layer = 10;
                LightCol.enabled = true;
                BaseMentObj.GetComponent<BoxCollider>().isTrigger = false;
                IsPlay = true;
                LeverDownAnim.SetTrigger("LeverDown");
                BaseMentAnim.SetTrigger("BaseMentDown");
                col.enabled = false;
               PlayerManager.Instance.playerMove.HangingOff();
            }
            yield return null;
        }
    }
}
