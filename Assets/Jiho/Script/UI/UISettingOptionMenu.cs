using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingOptionMenu : UIView
{
    public override void Toggle(bool value)
    {
        base.Toggle(value);

    }
        
    public void BackToMain()
    {
        Toggle(false);
    }
}
