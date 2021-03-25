using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefelctFound : MonoBehaviour
{
    public Transform StartToRaser;
    public Transform Target;

    public LayerMask ReflectObject;

    LineRenderer Line;

    void Start()
    {
        Line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 dir =  (Target.position - StartToRaser.position).normalized;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, ReflectObject))
        {
            Vector3 normalVector = hit.collider.gameObject.transform.forward;
            Vector3 reflectVector = Vector3.Reflect(dir, normalVector);
            reflectVector = reflectVector.normalized;

            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, hit.point);

            RaycastHit hit2;
            if(Physics.Raycast(hit.point, reflectVector, out hit2, Mathf.Infinity, ReflectObject))
            {
                Debug.Log("반사");
                Line.SetPosition(2, hit2.point);
            }

        }
        //Debug.DrawRay(transform.position, Target.position - StartToRaser.position, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 dir = (Target.position - StartToRaser.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, ReflectObject))
        {
            Vector3 normalVector = hit.collider.gameObject.transform.forward;
            Vector3 reflectVector = Vector3.Reflect(dir, normalVector);
            reflectVector = reflectVector.normalized;

            RaycastHit hit2;
            if (Physics.Raycast(hit.point, reflectVector, out hit2, ReflectObject))
            {
                //Debug.Log("반사");
                Gizmos.DrawRay(hit.point, reflectVector);
            }
            

        }
        Gizmos.DrawRay(transform.position, dir);
        
    }

}
