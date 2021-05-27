using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{
    Camera main;
    public Camera sub;

    public void Start()
    {
        main = CameraManager.Instance.MainCamera;
        main.clearFlags = CameraClearFlags.Depth;
        sub.clearFlags = CameraClearFlags.Depth;

        main.depth = -1;
        sub.depth = 0;
    }
}
