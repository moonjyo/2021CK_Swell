using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public bool hotSpotIsCentor = false;

    public Vector2 adjustHotSpot = Vector2.zero;

    private Vector2 hotspot;

    private void Start()
    {
      //  StartCoroutine(MyCursor());
    }

    IEnumerator MyCursor()
    {
        yield return new WaitForEndOfFrame();

        if (hotSpotIsCentor)//커서를 가운데 기본값
        {
            hotspot.x = cursorTexture.width / 2;
            hotspot.y = cursorTexture.height / 2;
        }
        else //아니면 새로 지정해줄수 있음 
        {
            hotspot = adjustHotSpot;
        }
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto); //커서세팅 

    }
}
