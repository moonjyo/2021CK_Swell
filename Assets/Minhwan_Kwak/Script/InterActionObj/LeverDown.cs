using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDown : MonoBehaviour
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



    private void OnCollisionEnter(Collision collision)
    {
          if ((1 << collision.transform.gameObject.layer & PlayerLayer) != 0)
         {
            StartCoroutine(InterActionRayCast());
        }
     }
    private IEnumerator InterActionRayCast()
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
