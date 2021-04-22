using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public UIMainMenu UIMainMenu;
    public UISettingOptionMenu UISettingOptionMenu;
    public UIFade UIFade;
    public Canvas InterActionUICanvas;
    public UISound uISound;
    public UIInventory uiInventory;

    [HideInInspector]
    public List<GameObject> AllInterActionUI = new List<GameObject>();


    private bool IsSettingMenu = false;


    public void Init()
    {
        uISound.Init();

        AudioManager.Instance.Change(0);
        AudioManager.Instance.Play();
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            IsSettingMenu = !IsSettingMenu;
            UISettingOptionMenu.Toggle(IsSettingMenu);
        }
        
    }
}
