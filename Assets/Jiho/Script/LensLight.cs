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

        // raycast 추가, 렌즈를 통과한 빛이 트리거를 주게하기 위함
        Physics.Raycast(pos, value, value.magnitude * 3);
    }
}
