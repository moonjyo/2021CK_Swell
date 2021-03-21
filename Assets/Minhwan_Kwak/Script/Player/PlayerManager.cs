using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerStatus playerStatus;
    public PlayerMove playerMove;
    public PlayerInput PlayerInput;
    public PlayerInteraction PlayerInteraction;
    public PlayerCliming playerCliming;
    public PlayerAnimationEvents playerAnimationEvents;

    public SizeModulate SizeModulate;


    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }
}
