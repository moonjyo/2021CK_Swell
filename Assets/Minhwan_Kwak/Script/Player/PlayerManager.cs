using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerStatus playerStatus;
    public PlayerMove playerMove;



    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
    }
 
}
