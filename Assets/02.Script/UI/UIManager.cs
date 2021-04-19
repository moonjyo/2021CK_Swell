﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIMainMenu UIMainMenu;
    public UISettingOptionMenu UISettingOptionMenu;
    public UIFade UIFade;
    public Canvas InterActionUICanvas;

    [HideInInspector]
    public List<GameObject> AllInterActionUI = new List<GameObject>();
}
