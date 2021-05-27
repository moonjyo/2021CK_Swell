using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISettingOptionMenu : UIView
{

    public override void Initialize()
    {
        base.Initialize();
        Toggle(false);
    }

    public override void Toggle(bool value)
    {
        base.Toggle(value);

    }

    public void ToggleOff(bool value)
    {
        base.Toggle(false);
    }




}
