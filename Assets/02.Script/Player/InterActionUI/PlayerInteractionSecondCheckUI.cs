using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionSecondCheckUI : MonoBehaviour
{
    public LayerMask InterActionLayer;

    [SerializeField]
    private LayerMask CameraActionLayer;

    private IInteractbale TargetObj = null;


    private void OnTriggerEnter(Collider other)
    {
        InterActionCheckIn(other);
      
    }


    private void OnTriggerExit(Collider other)
    {
        InterActionCheckOut(other);
            
    }

    public void InterActionCheckIn(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            TargetObj = other.GetComponent<IInteractbale>();
            if (TargetObj != null)
            {
                TargetObj.SecondInteractOn(); //현재 충돌된 interaction 에 second ui 들 on 
                if (!GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(TargetObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Add(TargetObj);

                }
            }
        }
    }

    public void InterActionCheckOut(Collider other)
    {
        if ((1 << other.gameObject.layer & InterActionLayer) != 0)
        {
            
            TargetObj = other.GetComponent<IInteractbale>();
            if (TargetObj != null)
            {

                PlayerManager.Instance.playerMove.InterActionUIPointUp();
                TargetObj.SecondInteractOff(); // 충돌된 second off 


                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(TargetObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(TargetObj);
                }


                if (PlayerManager.Instance.playerMove.InterActionrb != null)
                {
                    if (PlayerManager.Instance.playerMove.InterActionrb.gameObject == other.gameObject)
                    {
                        PlayerManager.Instance.playerMove.InterActionrb = null;
                    }
                }
            }
        }
    }
}
