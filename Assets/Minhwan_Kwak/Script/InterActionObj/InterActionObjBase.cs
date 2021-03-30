using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjBase : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    [HideInInspector]
    public BoxCollider Col;


    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        Col = transform.GetComponent<BoxCollider>();
    }


}
