using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDown : MonoBehaviour
{
    public Animator LeverDownAnim;
    public GameObject BaseMentObj;
    public Animator BaseMentAnim;
    private BoxCollider col;

    public LayerMask PlayerLayer;

    private bool IsPlay = false;


    private void Start()
    {
        col = transform.GetComponent<BoxCollider>();
    }

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
                yield return new WaitForSeconds(0.4f);
                BaseMentObj.layer = 10;
                IsPlay = true;
                LeverDownAnim.SetTrigger("LeverDown");
                BaseMentAnim.SetTrigger("BaseMentDown");
               PlayerManager.Instance.playerMove.HangingOff();
               col.isTrigger = false;
            }
            yield return null;
        }
    }
}
