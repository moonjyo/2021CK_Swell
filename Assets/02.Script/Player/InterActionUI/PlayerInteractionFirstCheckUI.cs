using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionFirstCheckUI : MonoBehaviour
{
    [SerializeField]
    private LayerMask InterActionLayer;

    private PlayerInterActionObj TargetObj = null;

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            TargetObj = other.GetComponent<PlayerInterActionObj>();
            // 현재 first interaction ui가 충돌 안됐을경우 시작
            if (TargetObj != null && !GameManager.Instance.uiManager.IsOnFirstInterActionUI)
            {
                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Count == 0)
                {
                    TargetObj.UIFirstObj.gameObject.SetActive(true);
                }
                //충돌된 ui firstobj 넣어줌 
                if (!GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Contains(TargetObj.UIFirstObj))
                {
                    GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Add(TargetObj.UIFirstObj);
                }
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
                //충돌끝난 uifirst obj off 
                TargetObj.UIFirstObj.gameObject.SetActive(false);

                //충돌끝난 ui first obj 넣어줌 
                if (GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Contains(TargetObj.UIFirstObj))
                {
                    GameManager.Instance.uiManager.OnActiveFirstInterActionUI.Remove(TargetObj.UIFirstObj);
                }
            }
        }

    }



}
