﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;
using Boo.Lang;
using System;
using UnityEngine.UI;


public class UIInventory : UIView
{
    public GameObject InventoryPanel;
    
    public UIInventoryElement[] ItemImageIcon = new UIInventoryElement[7];
    List<PlayerInterActionObj> ItemIconData = new List<PlayerInterActionObj>();
    [HideInInspector]
    public UIInventoryElement CurrentItemIcon;
    UIInventoryElement CombineItemIcon;

    Vector2 mousePos;

    [HideInInspector]
    public bool IsInventoryWindowOpen = false;
    bool IsSelectItemIcon = false;

    Vector2 ClickOffset;

    public Sprite EmptySprite;
    public Sprite[] ItemImage = new Sprite[3]; // 아이템 아이콘 이미지들

    GraphicRaycaster GraphicRay;
    PointerEventData Pointer;
    System.Collections.Generic.List<RaycastResult> resultsRay = new System.Collections.Generic.List<RaycastResult>();

    public ObserveMode ob;

    public LayerMask ObserveObjLayerMask;

    public DistinguishItem Distinguish;

    public delegate void DelDistinguish(GameObject Obj);
    public DelDistinguish Del;

    public Action act;

    private void Start()
    {
        GraphicRay = this.GetComponent<Canvas>().GetComponent<GraphicRaycaster>();
        Pointer = new PointerEventData(null);

        Distinguish.init();
    }
    public void SetMousePosVal(Vector2 value)
    {
        mousePos = value;
    }

    public Vector2 GetMousePosVal()
    {
        return mousePos;
    }

    public void EnterInventoryWindow()
    {
        if(IsInventoryWindowOpen)
        {
            return;
        }
        Debug.Log("Inventroy On");
        //InventoryPanel.SetActive(true);
        IsInventoryWindowOpen = true;
        //InventoryPanel.transform.DOMoveY(InventoryMovePosY, 0.5f);
        //InventoryPanel.GetComponent<RectTransform>().DOMoveY(1080, 0.3f);
        InventoryPanel.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.2f);
    }

    public void ExitInventoryWindow()
    {
        if (!IsInventoryWindowOpen || IsSelectItemIcon)
        {
            return;
        }

        Debug.Log("Inventroy Off");
        StartCoroutine(WaitForExitInventory());

    }
    IEnumerator WaitForExitInventory()
    {
        //InventoryPanel.gameObject.GetComponent<RectTransform>().DOMoveY(1208f, 0.3f);
        InventoryPanel.gameObject.GetComponent<RectTransform>().DOAnchorPosY(128.0f, 0.2f);
        yield return new WaitForSeconds(0.4f);
        IsInventoryWindowOpen = false;
        //InventoryPanel.SetActive(false);
    }

    public void DownItemIcon()
    {
        ClickOffset = CurrentItemIcon.CalculateOffsetMousePos(mousePos.x, mousePos.y);
    }

    public void DragItemIcon()
    {
        CurrentItemIcon.GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;

    }

    public void DropItemIcon()
    {
        CurrentItemIcon.GetComponent<RectTransform>().anchoredPosition = CurrentItemIcon.GetOriginPos();
        //IsSelectItemIcon = false;
        
        // Drop 했을 때 레이캐스트를 쏘아서 레이어를 파악하고 상호작용할지 그냥 되돌릴지 정하면 된다.
        RaycastHit hit;
        //Ray ray = CameraManager.Instance.MainCamera.ScreenPointToRay(mousePos); // 카메라는 매니저로 이동하기
        Ray ray = CameraManager.Instance.ObserveCamera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ObserveObjLayerMask))
        {
            Debug.Log(hit.collider.name);
            if((1 << hit.transform.gameObject.layer) == ObserveObjLayerMask) //상호작용 레이어로 교체해야함
            {
                if(CurrentItemIcon.HaveItem.InteractObjKey == hit.collider.name) // 오브젝트에 상호작용할 오브젝트 변수를 인스펙터로 주어지게하기
                {
                    if (Distinguish.DistinguishItemDic.TryGetValue(CurrentItemIcon.HaveItem.InteractObjKey, out Action<GameObject> value))
                    {
                        //value(hit.transform.gameObject);
                        value(hit.collider.gameObject);

                        CurrentItemIcon.IsInteract = true;
                        for (int i = 0; i < ItemIconData.Count; i++)
                        {
                            if (ItemImageIcon[i].IsInteract)
                            {
                                for (int j = i; j < ItemIconData.Count - 1; j++)
                                {
                                    ItemImageIcon[j].ElementImage.sprite = ItemImageIcon[j + 1].ElementImage.sprite;
                                    ItemImageIcon[j].HaveItem = ItemImageIcon[j + 1].HaveItem;
                                    ItemIconData[j] = ItemIconData[j + 1];
                                    //데이터 옮겨오면서 image도 같이 옮겨가야함
                                }
                            }
                        }
                        CurrentItemIcon.IsInteract = false;
                        ItemIconData.Pop();
                        ItemImageIcon[ItemIconData.Count].ElementImage.sprite = EmptySprite;

                        //ob.DeactivateObserverItem();
                    }
                }
            }
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Pointer.position = mousePos;
            GraphicRay.Raycast(Pointer, resultsRay);
            if(resultsRay.Count > 0)
            {
                CombineItemIcon = resultsRay[0].gameObject.GetComponent<UIInventoryElement>();
                bool CheckUIIcon = false;
                foreach (UIInventoryElement ui in ItemImageIcon)
                {
                    if (CombineItemIcon == ui && CombineItemIcon != CurrentItemIcon)
                    {
                        CheckUIIcon = true;
                    }
                }
                if (!CheckUIIcon)
                {
                    CombineItemIcon = null;
                }
            }           
            resultsRay.Clear();
        }
        ExitInventoryWindow();
    }

    public void GetItemIcon(PlayerInterActionObj Object) // 아이템을 얻었을 때
    {
        if(ItemIconData.Count >= 7)
        {
            return;
        }

        EnterInventoryWindow();

        ItemIconData.Add(Object);

        ItemImageIcon[ItemIconData.Count - 1].ElementImage.sprite = ItemImage[0]; // Object의 이미지 출력
        //ItemImageIcon[ItemIconData.Count - 1].ElementImage.sprite = Object.InventoryIcon;


        ItemImageIcon[ItemIconData.Count - 1].HaveItem = Object;

        StartCoroutine(WaitForGetItem());
        
    }

    IEnumerator WaitForGetItem()
    {
        yield return new WaitForSeconds(0.8f);
        ExitInventoryWindow();
    }

    public void CombineItem()
    {
        if(!IsSelectItemIcon || ItemIconData.Count < 2 || CombineItemIcon == null)
        {
            IsSelectItemIcon = false;
            return;
        }
        IsSelectItemIcon = false;

        int CurIndex = 0, ComIndex = 0;
        for(int i = 0; i < ItemIconData.Count; i++)
        {
            if(ItemIconData[i] == CurrentItemIcon.HaveItem)
            {
                CurIndex = i;
            }
            if(ItemIconData[i] == CombineItemIcon.HaveItem)
            {
                ComIndex = i;
            }
        }

        for (int i = CurIndex; i < ItemIconData.Count - CurIndex - 1; i++)
        {
            ItemImageIcon[i].ElementImage.sprite = ItemImageIcon[i].ElementImage.sprite;
            ItemIconData[i] = ItemIconData[i + 1];
        }
        //ItemIconData.Remove(CurrentItemIcon.HaveItem);
        //ItemIconData.Remove(CombineItemIcon.HaveItem);
        // 아이템 조합 함수?
        if(CurIndex > ComIndex) // 상대적 오른쪽에있는 아이콘을 왼쪽아이템으로 조합시도
        {
            //ItemIconData.Insert(ComIndex, DummyItemObj); // 조합한 아이템 insert
            //조합된 아이템 이미지 ComIndex 자리에 배치
        }
        else if(ComIndex > CurIndex) // 상대적 왼쪽에있는 아이콘을 오른아이템으로 조합시도
        {
            //ItemIconData.Insert(CurIndex, DummyItemObj); // 조합한 아이템 insert
            //조합된 아이템 이미지 sprite CurIndex 자리에 배치
        }
    }

    public void ClickItemIcon(string KeyName , PlayerInterActionObj Target)
    {
        if(!IsSelectItemIcon)
        {
            PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
            //ob.ActivateObserverItem(CurrentItemIcon.HaveItem.Key)
            ob.ActivateObserverItem(KeyName , Target);
        }
    }

    public void ClickItemIcon()
    {
        if (!IsSelectItemIcon)
        {
            ClickItemIcon(CurrentItemIcon.HaveItem.ItemKey, null);
        }
    }
}