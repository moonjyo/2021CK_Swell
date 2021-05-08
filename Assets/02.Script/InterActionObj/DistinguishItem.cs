using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DistinguishItem : MonoBehaviour
{
    public Dictionary<string, Action<GameObject>> DistinguishItemDic = new Dictionary<string, Action<GameObject>>();
    // 연출될 옵젝과 클릭될 옵젝에 Tag를 지정하여 Tag가 있는 오브젝트만 찾은 뒤 Dictionary에 담아둔다? FindTag사용 오브젝트풀로 미리 생성?
    public Dictionary<string, GameObject> ProductionClickItem = new Dictionary<string, GameObject>();

    bool IsInWood = false;
    bool IsInMatchStick = false;
    bool IsInWaxCube = false;

    public void init()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("ProductionInteractionObj");

        for(int i =0; i < go.Length; i ++)
        {
            ProductionClickItem.Add(go[i].name, go[i]);
        }

        DistinguishItemDic.Add("MSG_Lr_lokerGreen_1", InteractGreenLocker); // 상호작용이 되는 오브젝트 이름을 키로잡음
        DistinguishItemDic.Add("MSG_Lr_ringcaseGreen_1(Clone)", ShowpasswordUI);
        DistinguishItemDic.Add("MSG_Lr_owlstaute_head", JewelyinOwlEye);
        DistinguishItemDic.Add("MSG_Lr_woodstorage_door_1 1", OpenWoodBox);
        DistinguishItemDic.Add("Pivot_woodstorage(Clone)", ClickWood);
        DistinguishItemDic.Add("MSG_Lr_fireplace_1", InteractFirePlace);
        
    }

    public void InteractGreenLocker(GameObject Obj) // 초록색 좌물쇠 열었을 때
    {
        Debug.Log("GreenLocker Open");

        GameObject go = GameManager.Instance.uiManager.uiInventory.ob.GO;

        Transform[] GO = go.GetComponentsInChildren<Transform>();
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
            else if (value.name == "MSG_Lr_ringcaseGreen_1")
            {
                RingCase = value.gameObject;
            }
        } // 오브젝트 풀링 적용 이후 dictionary에 담아두고 찾아오기

        //LockerRing.DOMoveY(LockerRing.transform.position.y + 0.06f, 0.4f).OnComplete(() => Locker.gameObject.SetActive(false)).OnComplete(() => OpenBox.DOMoveX(OpenBox.transform.position.x - 1.0f, 1.0f));

        Obj.SetActive(false);

        // 머테리얼이 나오면 서서히 사라지게 설정
        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.uiInventory.ob.ActivateObserverItem(RingCase.name, RingCase.GetComponent<PlayerInterActionObj>());

        GameManager.Instance.uiManager.uiInventory.GetItemIcon(RingCase.GetComponent<PlayerInterActionObj>());
        //GameManager.Instance.uiManager.uiInventory.GetItemIcon(RingCase.GetComponent<PlayerInterActionObj>()); // 같이있던 학회사진 획득

        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        //Obj.GetComponent<PlayerInterActionObj>().SecondInteractOff();

       
    }

    public void ShowpasswordUI(GameObject Obj) // 반지 케이스 자리 클릭
    {
        // 비밀번호 ui창 보여주기
        GameManager.Instance.uiManager.uiRingCasePassword.Toggle(true);
    }

    public void JewelyinOwlEye(GameObject Obj) // 부엉이 동상 눈에 보석맞췄을 때
    {
        // 연출 : 머리가 열린다, 열쇠가 보인다.
        // 기능 : 장작보관함 열쇠 획득
        ProductionClickItem.TryGetValue("MSG_Lr_keyBrown_1", out GameObject go);
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(go.GetComponent<PlayerInterActionObj>());
    }

    public void OpenWoodBox(GameObject Obj)
    {
        // 연출, 기능 : 장작보관함이 열린다.
        ProductionClickItem.TryGetValue("MSG_Lr_woodstorage_door_1 1", out GameObject obj); // 게임 씬 내 오브젝트
        obj.SetActive(false);

        GameObject go = GameManager.Instance.uiManager.uiInventory.ob.GO;
        Transform[] GO = go.GetComponentsInChildren<Transform>();
        foreach(Transform value in GO)
        {
            if(value.name == "MSG_Lr_woodstorage_door_1 1")
            {
                value.gameObject.SetActive(false);
            }
            else if(value.name == "MSG_Lr_woodstorage_door_1")
            {
                //회전될놈
            }
        } // 오브젝트 풀링 적용 이후 dictionary에 담아두고 찾아오기
    }

    public void ClickWood(GameObject Obj) // 장작 보관함이 열린 후 장작을 터치했을 때
    {
        // 연출 : ??
        // 기능 : 장작 획득
        ProductionClickItem.TryGetValue("MSG_Lr_wood_1", out GameObject obj);
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(obj.GetComponent<PlayerInterActionObj>());
    }

    public void InteractFirePlace(GameObject Obj) // 벽난로 상호작용
    {
        if (GameManager.Instance.uiManager.uiInventory.CurrentItemIcon.HaveItem.name == "MSG_Lr_wood_1") // 장작 넣었을 때
        {
            IsInWood = true;
            // 벽난로에 장작 생성
            return;
        }
        else if (!IsInWood)
            return;

        if (GameManager.Instance.uiManager.uiInventory.CurrentItemIcon.HaveItem.name == "MSG_Lr_matchstick")// 성냥갑 넣었을 때
        {
            IsInMatchStick = true;
            // 벽난로에 불

            return;
        }
        else if (!IsInMatchStick)
            return;

        if (GameManager.Instance.uiManager.uiInventory.CurrentItemIcon.HaveItem.name == "MSG_Lr_Waxcube") // 밀랍큐브 넣었을 때
        {
            IsInWaxCube = true;
            // 연출 : 밀랍큐브 녹음
            // 기능 : 보라색 열쇠 획득


            ProductionClickItem.TryGetValue("MSG_Lr_keyPurple_1", out GameObject go);
            GameManager.Instance.uiManager.uiInventory.GetItemIcon(go.GetComponent<PlayerInterActionObj>());

            return;
        }
        else if (!IsInWaxCube)
            return;
        
    }

    public void InteractPurpleLocker(GameObject Obj) // 보라색 자물쇠 열었을 때
    {
        ProductionClickItem.TryGetValue("MSG_Lr_Phone", out GameObject phoneObj);
        ProductionClickItem.TryGetValue("MSG_Lr_articleMeteor", out GameObject MeteorObj);


        GameManager.Instance.uiManager.uiInventory.GetItemIcon(phoneObj.GetComponent<PlayerInterActionObj>());
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(MeteorObj.GetComponent<PlayerInterActionObj>());



        Obj.SetActive(false);
    }
}
