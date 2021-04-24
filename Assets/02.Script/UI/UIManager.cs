﻿using System.Collections;
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

    public List<FirstInterActionUI> OnActiveFirstInterActionUI = new List<FirstInterActionUI>(); //first ui obj list 


    public bool IsOnFirstInterActionUI = false;
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

    public void OnFirstInterActionUI()
    { 
      foreach (var Obj in OnActiveFirstInterActionUI)
        {
            Obj.gameObject.SetActive(true);
        }
    }

    public void OffFirstInterActionUI()
    {
        foreach (var Obj in OnActiveFirstInterActionUI)
        {
            Obj.gameObject.SetActive(false);
        }
    }
}
