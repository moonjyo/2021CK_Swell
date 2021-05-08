using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public UIMainMenu UIMainMenu;
    public UISettingOptionMenu UISettingOptionMenu;
    public UIFade UIFade;
    public Canvas InterActionUICanvas;
    public UISound uISound;
    public UIInventory uiInventory;
    public UIRingCasePassword uiRingCasePassword;

    public List<FirstInterActionUI> OnActiveFirstInterActionUI = new List<FirstInterActionUI>(); //first ui obj list 
    public List<PlayerInterActionObj> OnActiveSecondInterActionUI = new List<PlayerInterActionObj>(); //first ui obj list 
    public List<Image> AllImageList= new List<Image>(); //first ui obj list 

    public Dictionary<string, Image> DialogueImageDicL = new Dictionary<string, Image>();
    public Dictionary<string, Image> DialogueImageDicR = new Dictionary<string, Image>();

    public MonologueText monologueText;
    public DialogueText DialogueText;

    public bool IsOnFirstInterActionUI = false;
    private bool IsSettingMenu = false;


    private void Awake()
    {
        GameObject Canvas = GameObject.Find("BaseUICanvas");
        if (Canvas)
        {
            InterActionUICanvas = Canvas.GetComponent<Canvas>();
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {

        //uISound.Init();

        DialougeImageInit();
        //AudioManager.Instance.Change(0);
        //AudioManager.Instance.Play();
    }

    public void DialougeImageInit()
    {
        for(int i = 0; i < AllImageList.Count; ++i)
        {
            Image Target = Instantiate(AllImageList[i] , transform); //임시로 부모지정 
            Target.name =  Target.name.Replace("(Clone)","");
            Target.gameObject.SetActive(false);
            if(Target.name.Contains("R")) //해당 단어가 있으면 r 없으면 l 따라서 name이 매우 중요 
            {
                DialogueImageDicR.Add(Target.name , Target);
            }
            else
            {
                DialogueImageDicL.Add(Target.name, Target);
            }
        }
    }

    public void OnEsc(InputAction.CallbackContext context)
    {
       if(context.performed)
        {
            IsSettingMenu = !IsSettingMenu;
            UISettingOptionMenu.Toggle(IsSettingMenu);
        }
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            uiRingCasePassword.RingCaseOpen();
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
