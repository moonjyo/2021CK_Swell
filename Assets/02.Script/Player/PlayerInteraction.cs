using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask InterActionLayer;


    [SerializeField]
    private float radius;


    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, Vector3.up, 0f,InterActionLayer);


        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; ++i)
            {
                IInteractbale InterAct = hits[i].transform.GetComponent<IInteractbale>();
                if (InterAct == null) continue;
                InterAct.Interact();
            }
        }
        else
        {
            foreach (var target in GameManager.Instance.uiManager.AllInterActionUI)
            {
                target.SetActive(false);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, radius);
        
    }

}
