using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/Player Data" , order = int.MaxValue)]
public class PlayerData : ScriptableObject
{

    public float jumpspeed = 8.0f;

    public float Gravity = 20f;

    public float GravityAcceleration = 12f;

    public float PullSpeed;

    public float PushSpeed;

    public float WalkSpeed;

    public float WalkSoundTIme = 1f;

 
}
