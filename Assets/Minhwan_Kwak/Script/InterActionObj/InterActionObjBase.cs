using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjBase : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public BoxCollider Col;

    IEnumerator FollowCorutine;


    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        Col = transform.GetComponent<BoxCollider>();
        FollowCorutine = FollowMove();
    }

    public bool FollowOn()
    {
        StartCoroutine(FollowCorutine);
        return true;
    }
    public bool FollowOff()
    {
        StopCoroutine(FollowCorutine);
        return true;    
    }
     

    public IEnumerator FollowMove()
    {
        while(true)
        {
            rb.transform.position = PlayerManager.Instance.playerMove.InterActionObjTr.position;

            yield return null;
        }
    }
}
