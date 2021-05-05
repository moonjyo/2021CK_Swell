using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DistinguishItem : MonoBehaviour
{
    public Dictionary<string, Action<GameObject>> DistinguishItemDic = new Dictionary<string, Action<GameObject>>();

    public void init()
    {
        DistinguishItemDic.Add("MSG_Lr_lokerGreen_1", InteractGreenLocker); // 상호작용이 되는 오브젝트 이름을 키로잡음
        DistinguishItemDic.Add("MSG_Lr_ringcaseGreen_1(Clone)", ShowpasswordUI);
        
    }

    public void InteractGreenLocker(GameObject Obj)
    {
        Debug.Log("GreenLocker Open");

        Transform[] GO = Obj.GetComponentsInChildren<Transform>();
        Transform LockerRing = null;
        Transform OpenBox = null;
        Transform Locker = null;
        GameObject RingCase = null;
        foreach (Transform value in GO)
        {
            if (value.name == "MSG_Lr_lokerGreen_4")
            {
                LockerRing = value;
            }
            else if (value.name == "1")
            {
                OpenBox = value;
            }
            else if (value.name == "MSG_Lr_lokerGreen_1")
            {
                Locker = value;
            }
            else if(value.name == "MSG_Lr_ringcaseGreen_1")
            {
                RingCase = value.gameObject;
            }
        }

        //LockerRing.DOMoveY(LockerRing.transform.position.y + 0.06f, 0.4f).OnComplete(() => Locker.gameObject.SetActive(false)).OnComplete(() => OpenBox.DOMoveX(OpenBox.transform.position.x - 1.0f, 1.0f));
       
        // 머테리얼이 나오면 서서히 사라지게 설정
        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.uiInventory.ob.ActivateObserverItem(RingCase.name, RingCase.GetComponent<PlayerInterActionObj>());

        GameManager.Instance.uiManager.uiInventory.GetItemIcon(RingCase.GetComponent<PlayerInterActionObj>());

        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        Obj.GetComponent<PlayerInterActionObj>().SecondInteractOff();


    }

    public void ShowpasswordUI(GameObject Obj)
    {
        // 비밀번호 ui창 보여주기
        //GameManager.Instance.uiManager.
    }
}
