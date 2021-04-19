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
    
    public GameObject[] ItemImageIcon = new GameObject[5];
    public List<GameObject> ItemIconImage = new List<GameObject>();
    Vector3[] OriginIconPos = new Vector3[5];

    Vector2 mousePos;

    bool IsInventoryWindowOpen = false;
    bool IsSelectItemIcon = false;

    Vector2 ClickOffset;

    private void Start()
    {
        int i = 0;
        foreach(GameObject image in ItemImageIcon)
        {
            ItemIconImage.Add(image);
            OriginIconPos[i] = image.GetComponent<RectTransform>().anchoredPosition;
            i++;
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

    //public void DownItemIcon()
    //{
        //if()  클릭한 인벤토리아이템을 지정하여 알 수 있게하는 조건?
        // PointEnter일 때 전부 다 계산해서 값 지정하기?
    //}

    public void DownItemIcon1()
    {
        float Xvalue = mousePos.x - ItemIconImage[0].GetComponent<RectTransform>().position.x;
        float Yvalue = mousePos.y - ItemIconImage[0].GetComponent<RectTransform>().position.y;
        ClickOffset = new Vector2(Xvalue, Yvalue);
    }
    public void DownItemIcon2()
    {
        float Xvalue = mousePos.x - ItemIconImage[1].GetComponent<RectTransform>().position.x;
        float Yvalue = mousePos.y - ItemIconImage[1].GetComponent<RectTransform>().position.y;
        ClickOffset = new Vector2(Xvalue, Yvalue);
    }
    public void DownItemIcon3()
    {
        float Xvalue = mousePos.x - ItemIconImage[2].GetComponent<RectTransform>().position.x;
        float Yvalue = mousePos.y - ItemIconImage[2].GetComponent<RectTransform>().position.y;
        ClickOffset = new Vector2(Xvalue, Yvalue);
    }
    public void DownItemIcon4()
    {
        float Xvalue = mousePos.x - ItemIconImage[3].GetComponent<RectTransform>().position.x;
        float Yvalue = mousePos.y - ItemIconImage[3].GetComponent<RectTransform>().position.y;
        ClickOffset = new Vector2(Xvalue, Yvalue);
    }
    public void DownItemIcon5()
    {
        float Xvalue = mousePos.x - ItemIconImage[4].GetComponent<RectTransform>().position.x;
        float Yvalue = mousePos.y - ItemIconImage[4].GetComponent<RectTransform>().position.y;
        ClickOffset = new Vector2(Xvalue, Yvalue);
    }

    public void DragItemIcon1()
    {
        
        ItemIconImage[0].GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;
        
    }
    public void DragItemIcon2()
    {
        ItemIconImage[1].GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;
    }
    public void DragItemIcon3()
    {
        ItemIconImage[2].GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;
    }
    public void DragItemIcon4()
    {
        ItemIconImage[3].GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;
    }
    public void DragItemIcon5()
    {
        ItemIconImage[4].GetComponent<RectTransform>().position = mousePos - ClickOffset;
        IsSelectItemIcon = true;
    }

    public void DropItemIcon1()
    {
        ItemIconImage[0].GetComponent<RectTransform>().anchoredPosition = OriginIconPos[0];
        IsSelectItemIcon = false;
        ExitInventoryWindow();
        // Drop 했을 때 레이캐스트를 쏘아서 레이어를 파악하고 상호작용할지 그냥 되돌릴지 정하면 된다.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePos); // 카메라는 매니저로 이동하기
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
        }
    }
    public void DropItemIcon2()
    {
        ItemIconImage[1].GetComponent<RectTransform>().anchoredPosition = OriginIconPos[1];
        IsSelectItemIcon = false;
        ExitInventoryWindow();
    }
    public void DropItemIcon3()
    {
        ItemIconImage[2].GetComponent<RectTransform>().anchoredPosition = OriginIconPos[2];
        IsSelectItemIcon = false;
        ExitInventoryWindow();
    }
    public void DropItemIcon4()
    {
        ItemIconImage[3].GetComponent<RectTransform>().anchoredPosition = OriginIconPos[3];
        IsSelectItemIcon = false;
        ExitInventoryWindow();
    }
    public void DropItemIcon5()
    {
        ItemIconImage[4].GetComponent<RectTransform>().anchoredPosition = OriginIconPos[4];
        IsSelectItemIcon = false;
        ExitInventoryWindow();
    }

    public void SetMousePosVal(Vector2 value)
    {
        mousePos = value;
    }
}
