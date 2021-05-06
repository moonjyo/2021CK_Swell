using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIRingCasePassword : UIView
{
    public InputField PasswordField;
    bool IsSuccesPassword = false;

    public void RingCasePasswordCheck()
    {
        if(PasswordField.text == "212")
        {
            IsSuccesPassword = true;
        }
    }

    public void RingCaseOpen() // EnterInput시에 실행시킬것
    {
        if (!IsSuccesPassword)
            return;

        Debug.Log("Ringcase Open");
        Toggle(false);
        GameManager.Instance.uiManager.uiInventory.ob.DeactivateObserverItem();
        // 연출 : 반지케이스 뚜껑 열림
        // 기능 : 작은 보석 획득
        //GameManager.Instance.uiManager.uiInventory.GetItemIcon()

    }
}
