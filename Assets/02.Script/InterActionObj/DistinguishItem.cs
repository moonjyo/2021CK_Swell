using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class DistinguishItem : MonoBehaviour
{
    public Dictionary<string, Action<GameObject>> DistinguishItemDic = new Dictionary<string, Action<GameObject>>();
    // 연출될 옵젝과 클릭될 옵젝에 Tag를 지정하여 Tag가 있는 오브젝트만 찾은 뒤 Dictionary에 담아둔다? FindTag사용 오브젝트풀로 미리 생성?
    public Dictionary<string, GameObject> ProductionClickItem = new Dictionary<string, GameObject>(); // 연출할 아이템 담아두는 변수
    public Dictionary<string, GameObject> ClickGetItem = new Dictionary<string, GameObject>(); // 클릭할 아이템 담아두는 변수

    bool IsInWood = false;
    bool IsInMatchStick = false;
    bool IsInWaxCube = false;

    int numberInTrashcan = 0;
    int numberInWoodStorage = 0;

    //public BoxCollider InterActionStove;
    public void init()
    {
        GameObject[] ProductionObject = GameObject.FindGameObjectsWithTag("ProductionInteractionObj");

        for(int i =0; i < ProductionObject.Length; i ++)
        {
            ProductionClickItem.Add(ProductionObject[i].name, ProductionObject[i]);
            if(ProductionObject[i].name == "Plane012 (1)" || ProductionObject[i].name == "MSG_BGLR_Tennisball_1" || ProductionObject[i].name == "MSG_BGLR_FramePuzzlePiece" || ProductionObject[i].name == "NewsPaper" || ProductionObject[i].name == "MSG_BGLR_hat_1 (2)")
            {
                ProductionObject[i].SetActive(false);
            }
        }

        //GameObject[] ClickGetObject = GameObject.FindGameObjectsWithTag("ClickGetObject");

        //for (int i = 0; i < ClickGetObject.Length; i++)
        //{
        //    ClickGetItem.Add(ClickGetObject[i].name, ClickGetObject[i]);
        //}

        DistinguishItemDic.Add("MSG_Lr_lokerGreen_1", InteractGreenLocker); // 상호작용이 되는 오브젝트 이름을 키로잡음
        DistinguishItemDic.Add("MSG_Lr_ringcaseGreen_1(Clone)", ShowpasswordUI);
        DistinguishItemDic.Add("MSG_Lr_owlstaute_head", JewelyinOwlEye);
        DistinguishItemDic.Add("MSG_Lr_woodstorage_glass_1", OpenWoodBox);
        DistinguishItemDic.Add("Pivot_woodstorage(Clone)", ClickWood);
        DistinguishItemDic.Add("MSG_Lr_fireplace_1", InteractFirePlace);

        DistinguishItemDic.Add("MSG_Lr_clock_1(Clone)", ThrowInClock);

        DistinguishItemDic.Add("MSG_Lr_lokerPurple_1", InteractPurpleLocker);

        DistinguishItemDic.Add("MSG_Lr_roundtable_1(Clone)", InteractTable);
        DistinguishItemDic.Add("MSG_Lr_woodtorage_1(Clone)", TakeWood);
        DistinguishItemDic.Add("MSG_BGLR_decopictureframe_1(Clone)", PhotoFramePuzzle);
        DistinguishItemDic.Add("MSG_BGLR_umbrellastand_1 (1)(Clone)", ClearUpTrashCan);
        DistinguishItemDic.Add("MSG_Lr_standinghanger_1 (1)(Clone)", HangerInteraction);
        DistinguishItemDic.Add("Pivot_Door_Rm(Clone)", InteractDoor);

        //DistinguishItemDic.Add("테니스공", GetBall);
    }

    public void InteractGreenLocker(GameObject Obj) // 초록색 좌물쇠 열었을 때
    {
        Debug.Log("GreenLocker Open");

        GameObject go = GameManager.Instance.uiManager.uiInventory.ob.GO;

        Transform[] GO = go.GetComponentsInChildren<Transform>();
        Transform LockerRing = null;
        Transform OpenBox = null;
        Transform Locker = null;
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
        } // 오브젝트 풀링 적용 이후 dictionary에 담아두고 찾아오기

        //LockerRing.DOMoveY(LockerRing.transform.position.y + 0.06f, 0.4f).OnComplete(() => Locker.gameObject.SetActive(false)).OnComplete(() => OpenBox.DOMoveX(OpenBox.transform.position.x - 1.0f, 1.0f));

        Obj.SetActive(false);

        // 머테리얼이 나오면 서서히 사라지게 설정

        ProductionClickItem.TryGetValue("MSG_Lr_ringcaseGreen_1", out GameObject RingCaseObj);
        ProductionClickItem.TryGetValue("MSG_Lr_photoAcademy", out GameObject PhotoAcademy);

        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.uiInventory.ob.ActivateObserverItem(RingCaseObj.name, RingCaseObj.GetComponent<PlayerInterActionObj>());

       
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(PhotoAcademy.GetComponent<PlayerInterActionObj>()); // 같이있던 학회사진 획득
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(RingCaseObj.GetComponent<PlayerInterActionObj>());

        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);

        GameManager.Instance.uiManager.OffSecondInterActionUI();       
    }

    public void ShowpasswordUI(GameObject Obj) // 반지 케이스 자리 클릭
    {
        // 비밀번호 ui창 보여주기
        //GameManager.Instance.uiManager.uiRingCasePassword.Toggle(true);
    }

    public void JewelyinOwlEye(GameObject Obj) // 부엉이 동상 눈에 보석맞췄을 때
    {
        // 연출 : 머리가 열린다, 열쇠가 보인다.
        // 기능 : 장작보관함 열쇠 획득
        ProductionClickItem.TryGetValue("MSG_Lr_keyBrown_1", out GameObject go); // 장작보관함 열쇠로 따로 바꾸어야한다.
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(go.GetComponent<PlayerInterActionObj>());
    }

    public void OpenWoodBox(GameObject Obj)
    {
        // 연출, 기능 : 장작보관함이 열린다.
        ProductionClickItem.TryGetValue("MSG_Lr_woodstorage_glass_1", out GameObject obj); // 게임 씬 내 오브젝트
        obj.SetActive(false);

        GameObject go = GameManager.Instance.uiManager.uiInventory.ob.GO;
        Transform[] GO = go.GetComponentsInChildren<Transform>();
        foreach(Transform value in GO)
        {
            if(value.name == "MSG_Lr_woodstorage_glass_1") // 회전될 부모
            {
                value.gameObject.SetActive(false);
            }

        } // 오브젝트 풀링 적용 이후 dictionary에 담아두고 찾아오기
    }

    public void ClickWood(GameObject Obj) // 장작 보관함이 열린 후 장작을 터치했을 때
    {
        // 연출 : ??
        // 기능 : 장작 획득
        ProductionClickItem.TryGetValue("MSG_Lr_wood_1", out GameObject obj);
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(obj.GetComponent<PlayerInterActionObj>());
        Obj.SetActive(false);
        obj.SetActive(false);
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
        ProductionClickItem.TryGetValue("MSG_Lr_articleMeteor", out GameObject ArticleObj);


        GameManager.Instance.uiManager.uiInventory.GetItemIcon(phoneObj.GetComponent<PlayerInterActionObj>());
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(ArticleObj.GetComponent<PlayerInterActionObj>());


        Obj.SetActive(false);
    }

    public void InteractClock(GameObject Obj)
    {
        // 시침분침이 4시35분을 가리킬 때
        // 갈색열쇠 획득
        // 연출 : 시계 뚜껑이 열린다

        ProductionClickItem.TryGetValue("MSG_Lr_keyBrown_1", out GameObject go);
        GameManager.Instance.uiManager.uiInventory.GetItemIcon(go.GetComponent<PlayerInterActionObj>());
    }

    //=========================================================================================================//
    //=========================================================================================================//


    public void ThrowInClock(GameObject Obj) // 시계에 분침을 꽂았을 때
    {
        // 시계의 분침을 setactive false 해놓았다가 true로 전환
        if(ProductionClickItem.TryGetValue("Plane012 (1)", out GameObject MinuteHand))
        {
            MinuteHand.SetActive(true);
        }

        if(ProductionClickItem.TryGetValue("MSG_Lr_clock_1", out GameObject ClockObj))
        {
            ClockObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //ClockObj.gameObject.layer = 0;
            
            ClockObj.GetComponent<BoxCollider>().enabled = false;
           
        }

        //if (ProductionClickItem.TryGetValue("TennisBall", out GameObject BallObj)) // 테니스 공 찾아옴
        //{
        //    BallObj.SetActive(true);
        //    BallObj.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
        //}

        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.OffSecondInterActionUI();
      
    }

    public void FirePlace()
    {
        if (ProductionClickItem.TryGetValue("MSG_Lr_fireplace_1 (1)", out GameObject firePlace)) // 테니스 공 찾아옴
        {
            firePlace.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void InteractDoor(GameObject Obj)
    {
        //GameManager.Instance.stageManager.SceneChange("EndingStage1");
        SceneManager.LoadSceneAsync("EndingStage1", LoadSceneMode.Single);
    }

    public void InteractTable(GameObject Obj) // 신문 주워서 탁자에 올림
    {
        if(ProductionClickItem.TryGetValue("NewsPaper", out GameObject NewsPaperObj))
        {
            NewsPaperObj.SetActive(true);

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        }
    }

    public void TakeWood(GameObject Obj) // 떨어진 장작 치움?
    {
        numberInWoodStorage++;
        if(numberInWoodStorage == 3)
        {
            if (ProductionClickItem.TryGetValue("MSG_BGLR_key_1", out GameObject KeyObj))
            {
                GameManager.Instance.uiManager.uiInventory.GetItemIcon(KeyObj.GetComponent<PlayerInterActionObj>());

                GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
            }
        }
    }

    public void PhotoFramePuzzle(GameObject Obj) // 액자에 퍼즐 끼우기
    {
        if(ProductionClickItem.TryGetValue("MSG_BGLR_FramePuzzlePiece", out GameObject PuzzlePieceObj))
        {
            PuzzlePieceObj.SetActive(true);

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        }
    }

    public void TakeToBookShlef(GameObject Obj) // 책을 주워서 서랍장 정리
    {

    }

    public void ClearUpTrashCan(GameObject Obj) // 엎어진 쓰레기통 치우기
    {
        numberInTrashcan++;
        if(numberInTrashcan == 5)
        {
            if(ProductionClickItem.TryGetValue("MSG_BGLR_umbrellastand_1 (1)", out GameObject TrashCanObj))
            {
                TrashCanObj.transform.localRotation = Quaternion.Euler(0, 0, 0);

                GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
            }
        }
    }

    public void HangerInteraction(GameObject Obj)
    {
        if(ProductionClickItem.TryGetValue("MSG_BGLR_hat_1 (2)", out GameObject HatObj))
        {
            HatObj.SetActive(true);

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        }
    }
}
