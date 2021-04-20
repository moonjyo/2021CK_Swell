using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask InterActionLayer;


    [SerializeField]
    private float FirstRadius;

    [SerializeField]
    private float SecondRadius;

    public Transform FirstInterActionTr;
    public Transform SecondInterActionTr;

    private bool IsFirstCheck = false;

    private void Update()
    {
        FirstHitCheck();
        SecondHitCheck();
    }
    
    public void FirstHitCheck()
    {
        RaycastHit[] hits = Physics.SphereCastAll(FirstInterActionTr.position, FirstRadius, Vector3.up, 0f, InterActionLayer);

        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; ++i)
            {
                IInteractbale InterAct = hits[i].transform.GetComponent<IInteractbale>();
                if (InterAct == null) continue;
                    InterAct.FirstInteract();
                    IsFirstCheck = true;
            }
            foreach (var target in GameManager.Instance.uiManager.AllInterActionUI)
            {
                if (!target.CompareTag("SecondInterActionUI"))
                {
                    target.SetActive(false);
                }
            }
        }
        else
        {
            foreach (var target in GameManager.Instance.uiManager.AllInterActionUI)
            {
                if (!target.CompareTag("FirstInterActionUI"))
                {
                    target.SetActive(false);
                    IsFirstCheck = false;
                }
            }
        }
    }


    public void SecondHitCheck()
    {
        if (!IsFirstCheck)
        {
            RaycastHit[] hits = Physics.SphereCastAll(SecondInterActionTr.position, SecondRadius, Vector3.up, 0f, InterActionLayer);

            if (hits.Length != 0)
            {
                for (int i = 0; i < hits.Length; ++i)
                {
                    IInteractbale InterAct = hits[i].transform.GetComponent<IInteractbale>();
                    if (InterAct == null) continue;
                    InterAct.SecondInteract();
                }
            }
            else
            {
                foreach (var target in GameManager.Instance.uiManager.AllInterActionUI)
                {
                    if (!target.CompareTag("SecondInterActionUI"))
                    {
                        target.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(SecondInterActionTr.position, SecondRadius);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(FirstInterActionTr.position, FirstRadius);
    }

}
