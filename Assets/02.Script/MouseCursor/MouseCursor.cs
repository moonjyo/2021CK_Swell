using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public bool hotSpotIsCentor = false;

    public Vector2 adjustHotSpot = Vector2.zero;

    private Vector2 hotspot;

    public Sprite testsprite;

    private void Start()
    {
      MyCursor();

     DontDestroyOnLoad(this);
    }

    void MyCursor()
    {
        if (hotSpotIsCentor)//커서를 가운데 기본값
        {
            hotspot.x = testsprite.texture.width / 2;
            hotspot.y = testsprite.texture.height / 2;
        }
        else //아니면 새로 지정해줄수 있음 
        {
            hotspot = adjustHotSpot;
        }
        Cursor.SetCursor(testsprite.texture, hotspot, CursorMode.Auto); //커서세팅 
    }
}
