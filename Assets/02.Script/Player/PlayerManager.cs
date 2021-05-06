﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerStatus playerStatus;
    public PlayerMove playerMove;
    public PlayerInput PlayerInput;
    public PlayerInteractionFirstCheckUI PlayerInteractionFirstCheck;
    public PlayerInteractionSecondCheckUI PlayerInteractionSecondCheck;

    public PlayerAnimationEvents playerAnimationEvents;


   // public SizeModulate SizeModulate;
    //public FlashLight flashLight;
    public RefelctFound flashLight;



    public void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform);
    }

}
