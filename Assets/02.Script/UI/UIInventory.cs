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

    Vector2 mousePos;

    private void Start()
    {
        foreach(GameObject image in ItemImageIcon)
        {
            ItemIconImage.Add(image);
        }
    }

    public void EnterInventoryWindow()
    {
        Debug.Log("Inventroy On");
        InventoryPanel.SetActive(true);
        //InventoryPanel.transform.DOMoveY(InventoryMovePosY, 0.5f);
        InventoryPanel.GetComponent<RectTransform>().DOMoveY(1080, 0.3f);
    }

    public void ExitInventoryWindow()
    {
        Debug.Log("Inventroy Off");
        StartCoroutine(WaitForInventoryOff());

    }
    IEnumerator WaitForInventoryOff()
    {
        yield return InventoryPanel.gameObject.GetComponent<RectTransform>().DOMoveY(1208f, 0.3f);
        //InventoryPanel.SetActive(false);
    }

    public void DragItemIcon1()
    {
        ItemIconImage[0].GetComponent<RectTransform>().position = mousePos;
    }

    public void SetMousePosVal(Vector2 value)
    {
        mousePos = value;
    }
}
