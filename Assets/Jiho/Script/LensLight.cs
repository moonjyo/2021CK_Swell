using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensLight : MonoBehaviour
{
    public LineRenderer Line;

    private void Awake()
    {
        Line = GetComponent<LineRenderer>();
    }

    public void GetConcaveLens(Vector3 value, Vector3 pos)
    {
        Line.enabled = true;
        Line.SetPosition(0, pos);
        Line.SetPosition(1, pos + value * 3);

        // raycast 추가
    }
}
