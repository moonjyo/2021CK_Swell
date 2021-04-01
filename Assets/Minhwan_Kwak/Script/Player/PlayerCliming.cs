using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCliming : MonoBehaviour
{
    public Transform HeadHighTr;
    public Transform HeadLowTr;

    public float Distance = 0.2f;

    public bool IsHighCheck = false;
    public bool IsLowCheck = false;
    public LayerMask HangingLayer;

    private Vector2 ClimingVec;

    private void Update()
    {
        RaycastHit Highhit;
        RaycastHit Lowhit;
        bool isHitHigh = Physics.Raycast(HeadHighTr.position, HeadHighTr.forward, out Highhit, Distance, HangingLayer);
        bool isHitLow = Physics.Raycast(HeadHighTr.position, HeadLowTr.forward, out Lowhit, Distance, HangingLayer);


        if (!isHitHigh && isHitLow)
        {
            PlayerManager.Instance.playerMove.Hanging(ClimingVec);
        }
    }


    public void SetCliming(Vector2 value)
    {
        ClimingVec = value;
    }


    private void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHitHigh = Physics.Raycast(HeadHighTr.position, HeadHighTr.forward, out hit, Distance, HangingLayer);

        Gizmos.color = Color.red;
        if (isHitHigh)
        {
            Gizmos.DrawRay(HeadHighTr.position, HeadHighTr.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(HeadHighTr.position, HeadHighTr.forward * Distance);
        }

        bool isHitLow = Physics.Raycast(HeadHighTr.position, HeadHighTr.forward, out hit, Distance, HangingLayer);

        Gizmos.color = Color.yellow;
        if (isHitLow)
        {
            Gizmos.DrawRay(HeadLowTr.position, HeadLowTr.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(HeadLowTr.position, HeadLowTr.forward * Distance);
        }
    }
}
