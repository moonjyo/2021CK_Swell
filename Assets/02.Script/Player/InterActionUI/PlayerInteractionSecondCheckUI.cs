using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSecondCheckUI : MonoBehaviour
{
    [SerializeField]
    private LayerMask InterActionLayer;

    private PlayerInterActionObj TargetObj = null;


    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {

            TargetObj = other.GetComponent<PlayerInterActionObj>();
            if (TargetObj != null)
            {
                GameManager.Instance.uiManager.IsOnFirstInterActionUI = true; // start  firstinteraction ui exit 
                TargetObj.SecondInteractOn(); //현재 충돌된 interaction 에 second ui 들 on 
                GameManager.Instance.uiManager.OffFirstInterActionUI(); // 현재 충돌된 firstinteraction ui off 
            
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            TargetObj = other.GetComponent<PlayerInterActionObj>();
            if (TargetObj != null)
            {
                GameManager.Instance.uiManager.IsOnFirstInterActionUI = false; // firstinteraction ui 활성화
                TargetObj.SecondInteractOff(); // 충돌된 second off 
                GameManager.Instance.uiManager.OnFirstInterActionUI(); //다시 충돌중인 first interaction on 
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Push", false);
                PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetBool("Pull", false);

            }
        }

    }
}
