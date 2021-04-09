using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInterActionItem : MonoBehaviour
{
    
    [HideInInspector]
    public BoxCollider Col;

    public Rigidbody rb;

    public Vector3 ItemOffsetPos;
    
    public Vector3 moveDirection;
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
        Col.isTrigger = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        return true;
    }
}
