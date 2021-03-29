using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionObjBase : MonoBehaviour
{
    public Rigidbody rb;

    public int rbMass;

    private void Start()
    {
        rb.mass = rbMass;
    }


}
