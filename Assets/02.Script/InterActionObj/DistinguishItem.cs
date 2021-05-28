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

    int numberInTrashcan = 0;
    int numberInWoodStorage = 0;
    int numberInBookShelf = 0;

    //public BoxCollider InterActionStove;
    public void init()
    {
        GameObject[] ProductionObject = GameObject.FindGameObjectsWithTag("ProductionInteractionObj");

        for(int i =0; i < ProductionObject.Length; i ++)
        {
            ProductionClickItem.Add(ProductionObject[i].name, ProductionObject[i]);
            if(ProductionObject[i].name == "Plane012 (1)" || ProductionObject[i].name == "MSG_BGLR_Tennisball_1" || ProductionObject[i].name == "MSG_BGLR_FramePuzzlePiece" 
                || ProductionObject[i].name == "NewsPaper" || ProductionObject[i].name == "MSG_BGLR_hat_1 (2)" || ProductionObject[i].name =="MSG_BGLR_Book_1 (1)" ||
                ProductionObject[i].name == "MSG_BGLR_Book_2 (1)" || ProductionObject[i].name == "MSG_BGLR_Book_3 (1)" || ProductionObject[i].name == "MSG_BGLR_Book_4 (1)" ||
                ProductionObject[i].name == "MSG_BGLR_Book_5 (1)")
            {
                ProductionObject[i].SetActive(false);
            }
        }

        //GameObject[] ClickGetObject = GameObject.FindGameObjectsWithTag("ClickGetObject");

        //for (int i = 0; i < ClickGetObject.Length; i++)
        //{
        //    ClickGetItem.Add(ClickGetObject[i].name, ClickGetObject[i]);
        //}
        DistinguishItemDic.Add("MSG_Lr_clock_1(Clone)", ThrowInClock); // 상호작용이 되는 오브젝트 이름을 키로잡음
        DistinguishItemDic.Add("MSG_BGLR_smalltable_1(Clone)", InteractTable);
        DistinguishItemDic.Add("MSG_Lr_woodtorage_1(Clone)", TakeWood);
        DistinguishItemDic.Add("MSG_BGLR_decopictureframe_1(Clone)", PhotoFramePuzzle);
        DistinguishItemDic.Add("Pivot_BookShelf_1(Clone)", TakeToBookShlef);
        DistinguishItemDic.Add("MSG_BGLR_umbrellastand_1 (1)(Clone)", ClearUpTrashCan);
        DistinguishItemDic.Add("MSG_Lr_standinghanger_1", HangerInteraction);
        DistinguishItemDic.Add("Pivot_Door_Rm(Clone)", InteractDoor);
    }

    public void ThrowInClock(GameObject Obj) // 시계에 분침을 꽂았을 때
    {
        // 시계의 분침을 setactive false 해놓았다가 true로 전환
        if(ProductionClickItem.TryGetValue("Plane012 (1)", out GameObject MinuteHand))
        {
            MinuteHand.SetActive(true);
            MinuteHand.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
        }

        if(ProductionClickItem.TryGetValue("MSG_Lr_clock_1", out GameObject ClockObj))
        {
            ClockObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
            ClockObj.transform.localPosition += new Vector3(0, 0.132f, 0);
            //ClockObj.gameObject.layer = 0;

            SetActiveUI(ClockObj);

        }

        //if (ProductionClickItem.TryGetValue("TennisBall", out GameObject BallObj)) // 테니스 공 찾아옴
        //{
        //    BallObj.SetActive(true);
        //    BallObj.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
        //}

        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.OffSecondInterActionUI();
        ClockObj.layer = 0;

        GameManager.Instance.uiManager.AchiveMents(13f);

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
            NewsPaperObj.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
        }
        if (GameManager.Instance.uiManager.uiInventory.ob.ObserveObj.TryGetValue("MSG_BGLR_smalltable_1", out GameObject roundtable))
        {
            SetActiveUI(roundtable);
        }

        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        GameManager.Instance.uiManager.AchiveMents(11f);

    }

    public void TakeWood(GameObject Obj) // 떨어진 장작 치움?
    {
        numberInWoodStorage++;
        if(numberInWoodStorage == 3)
        {
                if (GameManager.Instance.uiManager.uiInventory.ob.ObserveObj.TryGetValue("MSG_Lr_woodtorage_1", out GameObject woodtorage))
                {
                    SetActiveUI(woodtorage);
                }
                GameManager.Instance.uiManager.OffSecondInterActionUI();

                GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
                GameManager.Instance.uiManager.AchiveMents(12f);
           
        }

    }

    public void PhotoFramePuzzle(GameObject Obj) // 액자에 퍼즐 끼우기
    {
        if(ProductionClickItem.TryGetValue("MSG_BGLR_FramePuzzlePiece", out GameObject PuzzlePieceObj))
        {
            PuzzlePieceObj.SetActive(true);
            PuzzlePieceObj.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
            if (GameManager.Instance.uiManager.uiInventory.ob.ObserveObj.TryGetValue("MSG_BGLR_decopictureframe_1", out GameObject decopictureframe))
            {
                SetActiveUI(decopictureframe);
            }

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        }


        GameManager.Instance.uiManager.AchiveMents(16f);
    }

    public void TakeToBookShlef(GameObject Obj) // 책을 주워서 서랍장 정리
    {
        numberInBookShelf++;
        string Name = GameManager.Instance.uiManager.uiInventory.CurrentItemIcon.HaveItem.name;

        Transform ChildObj = GameManager.Instance.uiManager.uiInventory.ob.GO.transform.Find(Name + " (1)");
        ChildObj.gameObject.SetActive(true);
        ChildObj.transform.Find(Name).gameObject.layer = 18;
        

        switch (Name)
        {
            case "MSG_BGLR_Book_1" :
                if(ProductionClickItem.TryGetValue(Name + " (1)", out GameObject BookObj1))
                {
                    BookObj1.SetActive(true);
                    BookObj1.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
                }
                break;

            case "MSG_BGLR_Book_2":
                if (ProductionClickItem.TryGetValue(Name + " (1)", out GameObject BookObj2))
                {
                    BookObj2.SetActive(true);
                    BookObj2.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
                }
                break;
            case "MSG_BGLR_Book_3":
                if (ProductionClickItem.TryGetValue(Name + " (1)", out GameObject BookObj3))
                {
                    BookObj3.SetActive(true);
                    BookObj3.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
                }
                break;

            case "MSG_BGLR_Book_4":
                if (ProductionClickItem.TryGetValue(Name + " (1)", out GameObject BookObj4))
                {
                    BookObj4.SetActive(true);
                    BookObj4.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
                }
                break;

            case "MSG_BGLR_Book_5":
                if (ProductionClickItem.TryGetValue(Name + " (1)", out GameObject BookObj5))
                {
                    BookObj5.SetActive(true);
                    BookObj5.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
                }
                break;

            default:
                return;
        }        

        if (numberInBookShelf == 5)
        {
            if (GameManager.Instance.uiManager.uiInventory.ob.ObserveObj.TryGetValue("MSG_BGLR_BookShelf_1", out GameObject Hanger))
            {
                SetActiveUI(Hanger);
            }

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
            GameManager.Instance.uiManager.AchiveMents(17f);
        }


       
    }

    public void ClearUpTrashCan(GameObject Obj) // 엎어진 쓰레기통 치우기
    {
        numberInTrashcan++;
        if(numberInTrashcan == 5)
        {
            if(ProductionClickItem.TryGetValue("MSG_BGLR_umbrellastand_1 (1)", out GameObject TrashCanObj))
            {
                TrashCanObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                TrashCanObj.transform.localPosition += new Vector3(0, 0.25f, 0);

                Transform[] ChildObj = TrashCanObj.GetComponentsInChildren<Transform>();

                for(int i = 1; i < ChildObj.Length; i++)
                {
                    ChildObj[i].localPosition = Vector3.zero;
                }

                SetActiveUI(TrashCanObj);
                GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();

                GameManager.Instance.uiManager.AchiveMents(11f);
            }
        }


    }

    public void HangerInteraction(GameObject Obj)
    {
        if(ProductionClickItem.TryGetValue("MSG_BGLR_hat_1 (2)", out GameObject HatObj))
        {
            HatObj.SetActive(true);
            HatObj.GetComponent<FMODUnity.StudioEventEmitter>().enabled = true;
            if (GameManager.Instance.uiManager.uiInventory.ob.ObserveObj.TryGetValue("MSG_Lr_standinghanger_1 (1)", out GameObject Hanger))
            {
                SetActiveUI(Hanger);
            }

            GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
            GameManager.Instance.uiManager.AchiveMents(10f);
        }


    }


    public void SetActiveUI(GameObject Obj)
    {
        if (Obj != null)
        {
            IInteractbale InteractObj = Obj.GetComponent<IInteractbale>();

            if (InteractObj != null)
            {
                if (GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Contains(InteractObj))
                {
                    GameManager.Instance.uiManager.OnActiveSecondInterActionUI.Remove(InteractObj);
                }
                Obj.GetComponent<BoxCollider>().enabled = false;
                GameManager.Instance.uiManager.OffSecondInterActionUI();
            }
        }
    }
}
