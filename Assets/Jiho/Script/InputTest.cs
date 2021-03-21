using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public SizeModulate sizeModulate;
    //public CameraAction cameraAction;

    public void OnRightMouseButton(InputAction.CallbackContext context)
    {
        // layermask를 활용해서 raycast로 클릭되면 sizemodulate의 변수로 넣어줌
        if (context.started)
        {
            Vector2 Input = Mouse.current.position.ReadValue();
            sizeModulate.ItemSelect(Input);
        }
    }

    public void OnSizeModulate(InputAction.CallbackContext context) // 
    {
        Vector2 value = context.ReadValue<Vector2>();
        switch (value.y)
        {
            case 120:
                Debug.Log("마우스휠업");

                break;
            case -120:
                Debug.Log("마우스휠다운");
                break;
        }
        if (value.y == 120f || value.y == -120f)
            sizeModulate.ItemSizeModulate(value.y);
    }
}
