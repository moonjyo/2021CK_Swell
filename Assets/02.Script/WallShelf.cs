using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShelf : MonoBehaviour
{
    private Animator anim;
    private BoxCollider col;

    public LayerMask PlayerLayer;

    private bool IsPlay = false;

    RaycastHit hit;

    public float Distance = 15f;

    private void Start()
    {
        col = transform.GetComponent<BoxCollider>();
        anim = transform.GetComponent<Animator>();
        StartCoroutine(InterActionRayCast());
    }
    

    private IEnumerator InterActionRayCast()
    {
        while(!IsPlay)
        {
            bool isHitHigh = Physics.Raycast(transform.position, -transform.forward, out hit, Distance, PlayerLayer);

            if (isHitHigh)
            {
                if ((1 << hit.transform.gameObject.layer & PlayerLayer) != 0)
                {
                    if (PlayerManager.Instance.playerAnimationEvents.PlayerAnim)
                    {
                        yield return new WaitForSeconds(0.7f);
                        transform.gameObject.layer = 10; // ground 
                        IsPlay = true;
                        anim.SetTrigger("Trigeron");
                        col.isTrigger = false;
                    }
                }
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHitHigh = Physics.Raycast(transform.position, -transform.forward, out hit, Distance, PlayerLayer);

        Gizmos.color = Color.red;
        if (isHitHigh)
        {
            Gizmos.DrawRay(transform.position, -transform.forward * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(transform.position, -transform.forward * Distance);
        }
    }

}
