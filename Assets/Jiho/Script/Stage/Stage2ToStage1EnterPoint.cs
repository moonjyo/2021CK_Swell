using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2ToStage1EnterPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 페이드연출 먼저 시켜줌
        // collision이 캐릭터일 때 Scene이동
        StageManager.Instance.EnterStage01();
    }
}
