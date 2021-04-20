using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;
using Boo.Lang;
using System;

public class UIInventory : UIView
{
    public GameObject InventoryPanel;
    
    public UIInventoryElement[] ItemImageIcon = new UIInventoryElement[5];
    public List<UIInventoryElement> ItemIconImage = new List<UIInventoryElement>();
    [HideInInspector]
    public UIInventoryElement CurrentItemIcon;

    Vector2 mousePos;

    bool IsInventoryWindowOpen = false;
    bool IsSelectItemIcon = false;

    Vector2 ClickOffset;

    public Sprite EmptySprite;

    private void Start()
    {
        foreach(UIInventoryElement image in ItemImageIcon)
        {
            ItemIconImage.Add(image);
        }
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
        InventoryPanel.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.3f);
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
        InventoryPanel.gameObject.GetComponent<RectTransform>().DOAnchorPosY(128.0f, 0.3f);
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
        IsSelectItemIcon = false;
        ExitInventoryWindow();
        // Drop 했을 때 레이캐스트를 쏘아서 레이어를 파악하고 상호작용할지 그냥 되돌릴지 정하면 된다.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos); // 카메라는 매니저로 이동하기
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.name == "Cube")
            {
                CurrentItemIcon.IsInteract = true;
                for (int i = 0; i < ItemIconImage.Count; i++)
                {
                    if (ItemImageIcon[i].IsInteract)
                    {
                        for (int j = i; j < ItemIconImage.Count - 1; j++)
                        {
                            ItemImageIcon[j].ElementImage.sprite = ItemImageIcon[j + 1].ElementImage.sprite;
                            ItemIconImage[j] = ItemIconImage[j + 1];
                            //데이터 옮겨오면서 image도 같이 옮겨가야함
                        }
                        
                    }
                }
                CurrentItemIcon.IsInteract = false;
                ItemIconImage.Pop();
                ItemImageIcon[ItemIconImage.Count].ElementImage.sprite = EmptySprite;
            }
        }
    }

    public void GetItemIcon() // 아이템을 얻었을 때
    {
        if(ItemIconImage.Count >= 5)
        {
            return;
        }

        //ItemIconImage.Add(item)
    }

    public void SetMousePosVal(Vector2 value)
    {
        mousePos = value;
    }
}
