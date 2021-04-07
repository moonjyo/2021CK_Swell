using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjBase : MonoBehaviour
{
    
    [HideInInspector]
    public BoxCollider Col;

    public Rigidbody rb;

    public Vector3 ItemOffsetPos;
    
    public Vector3 moveDirection;
    public float Gravity = 20f;
    public float GravityAcceleration = 12f;
    public bool IsGravity = false;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        Col = transform.GetComponent<BoxCollider>();
    }

    public bool FollowOn()
    {
        transform.parent = PlayerManager.Instance.playerMove.Root_Tr;
        transform.localPosition = ItemOffsetPos;
        Col.isTrigger = true;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        return true;
    }
    public bool FollowOff()
    {
        transform.parent = null;
        Col.isTrigger = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        return true;
    }
}
