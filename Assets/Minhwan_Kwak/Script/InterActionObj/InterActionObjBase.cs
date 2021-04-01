using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjBase : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public BoxCollider Col;

    private float CurrentMass;

    public Vector3 ItemOffsetPos;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        Col = transform.GetComponent<BoxCollider>();
        CurrentMass = rb.mass;
    }

    public bool FollowOn()
    {
        StartCoroutine(FollowMove());
        return true;
    }
    public bool FollowOff()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.transform.parent = null;
        rb.isKinematic = false;
        Col.isTrigger = false;
        rb.mass = CurrentMass;
        return true;
    }

    public IEnumerator FollowMove()
    {
        rb.transform.parent = PlayerManager.Instance.playerMove.Root_Tr;
        rb.isKinematic = true;
        rb.transform.localPosition = ItemOffsetPos;
        Col.isTrigger = true;
        rb.mass = 0.1f;



        yield return null;

    }
}
