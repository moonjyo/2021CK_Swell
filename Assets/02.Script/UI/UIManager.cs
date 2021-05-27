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
    //public UIRingCasePassword uiRingCasePassword;
    public UIPauseWindow uiPauseWindow;
    public UITimer uiTimer;

    public List<FirstInterActionUI> OnActiveFirstInterActionUI = new List<FirstInterActionUI>(); //first ui obj list 
    public List<IInteractbale> OnActiveSecondInterActionUI = new List<IInteractbale>(); //first ui obj list 
    public List<Image> AllImageList= new List<Image>(); //first ui obj list 

    public Dictionary<string, Image> DialogueImageDicL = new Dictionary<string, Image>();
    public Dictionary<string, Image> DialogueImageDicR = new Dictionary<string, Image>();

    public MonologueText monologueText;
    public DialogueText DialogueText;

    public bool IsOnFirstInterActionUI = false;
    //private bool IsSettingMenu = false;

    public Text AchiveMentText;

    public bool IsTimePuase = false;

    public GameObject PauseWindows;


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
       //if(context.performed)
       // {
       //     IsSettingMenu = !IsSettingMenu;
       //     UISettingOptionMenu.Toggle(IsSettingMenu);
       // }
       if(context.started)
        {
            uiPauseWindow.Toggle(!uiPauseWindow.gameObject.activeSelf);
        }
    }

    public void OnEnter(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //uiRingCasePassword.RingCaseOpen();
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
    public void OnSecondInterActionUI()
    {
        foreach (var Objs in OnActiveSecondInterActionUI)
        {
            foreach(var obj in Objs.GetUIObjList())
            {
                obj.gameObject.SetActive(true);
            }
        }
    }

    public void OffSecondInterActionUI()
    {
        foreach (var Objs in OnActiveSecondInterActionUI)
        {
            foreach (var obj in Objs.GetUIObjList())
            {
                obj.gameObject.SetActive(false);
            }
        }
    }


    public void AchiveMents(float Value)
    {
        float temp =  float.Parse(AchiveMentText.text);
        float Parse  = temp + Value;
        AchiveMentText.text = Parse.ToString();

        if(Parse == 100)
        {
            if (GameManager.Instance.uiManager.uiInventory.Distinguish.ProductionClickItem.TryGetValue("MSG_BGLR_Tennisball_1", out GameObject obj))
            {
                obj.SetActive(true);
            }
            GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.FIREPLACETIMELINE].SetActive(true);
        }
    }

    public void ClearRoom()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        PlayerManager.Instance.playerAnimationEvents.IsAnimStart = true;
        GameManager.Instance.uiManager.DialogueText.DialogueCount(8, 10);
        GameManager.Instance.uiManager.DialogueText.ShowDialogue();
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.FIREPLACE].transform.GetComponent<BoxCollider>().enabled = true;
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.CAMTRIGGER].SetActive(true);
        IsTimePuase = true;
    }
    
}
