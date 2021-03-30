using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractLaser : MonoBehaviour
{
    LineRenderer Line;

    public LayerMask RefractionObjLayerMask;

    RefractLaser Refract;

    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
    }

    public bool GetRefract(Vector3 value)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, value,out hit, Mathf.Infinity, RefractionObjLayerMask))
        {
            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, hit.point);

            Refract = hit.transform.gameObject.GetComponent<RefractLaser>();
            if(Refract.GetRefract(transform.forward))
            {
                Line.enabled = false;
            }
            return true;
        }
        else
        {
            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, transform.position + transform.forward * 5);

            return false;
        }

        
    }
}
