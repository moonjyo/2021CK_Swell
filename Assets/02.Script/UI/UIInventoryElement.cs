using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryElement : MonoBehaviour
{
    public Image ElementImage;
    //public Sprite ElementSprite;
    Vector2 OriginPos;

    UIInventory uiInventory;

    public bool IsInteract = false;

    // 가지고있는 아이템에 대한 정보변수 필요함
    public PlayerInterActionObj HaveItem;

    void Start()
    {
        OriginPos = this.GetComponent<RectTransform>().anchoredPosition;
        uiInventory = GetComponentInParent<UIInventory>();
        ElementImage = GetComponent<Image>();
        //ElementSprite = ElementImage.sprite;
    }

    public Vector2 CalculateOffsetMousePos(float x, float y)
    {
        float mouseX = x - this.GetComponent<RectTransform>().position.x;
        float mouseY = y - this.GetComponent<RectTransform>().position.y;
        return new Vector2(mouseX, mouseY);
    }

    public Vector2 GetOriginPos()
    {
        return OriginPos;
    }

    public void SetCurrentItemIcon()
    {
        uiInventory.CurrentItemIcon = this;
        uiInventory.DownItemIcon();
    }
}
