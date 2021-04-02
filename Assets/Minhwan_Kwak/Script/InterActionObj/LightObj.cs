using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObj : MonoBehaviour
{
    public LayerMask PlayerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & PlayerLayer) != 0)
        {
            PlayerManager.Instance.PlayerInput.IsLightGet = true;
            Destroy(gameObject);
        }
    }
}
