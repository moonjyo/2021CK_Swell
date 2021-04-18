using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using DG.Tweening;
using System.Collections;

public class UIInventory : UIView
{
    public GameObject InventoryPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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
        yield return InventoryPanel.gameObject.GetComponent<RectTransform>().DOMoveY(1208.37f, 0.3f);
        //InventoryPanel.SetActive(false);
    }
}
