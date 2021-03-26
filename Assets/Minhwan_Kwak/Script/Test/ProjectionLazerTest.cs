using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ProjectionLazerTest : MonoBehaviour
{
    public int MaxreflectionCount = 5;
    public float maxStepDistance = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        DrawpredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward , MaxreflectionCount);

    }

    private void DrawpredictedReflectionPattern(Vector3 position , Vector3 direciton , int reflectionRemaining)
    {
        if(reflectionRemaining == 0)
        {
            return;
        }
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direciton);
        RaycastHit hit;
        if(Physics.Raycast(ray , out hit , maxStepDistance))
        {
            direciton = Vector3.Reflect(direciton, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direciton * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition , position);

        DrawpredictedReflectionPattern(position , direciton , reflectionRemaining -1);

    }

 
}
