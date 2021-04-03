using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarStick : MonoBehaviour
{
    public LayerMask Stick;
    public GameObject BackGroundStick;
    private GameObject InterActionStick;

    public bool IsOnTriggerStick = false;

    Collider starCheckCollider;

    private void Start()
    {
        starCheckCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & Stick) != 0)
        {
            InterActionStick = other.gameObject;
            IsOnTriggerStick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & Stick) != 0)
        {
            IsOnTriggerStick = false;
        }

    }


    public void StartStickInterAction()
    {
        starCheckCollider.enabled = false;
        PlayerManager.Instance.PlayerInput.IsPickUpItem = false;
        PlayerManager.Instance.playerMove.SetRemoveInterActionObj();
        IsOnTriggerStick = false;
        StageManager.Instance.stage2.IsInStick = true;
        BackGroundStick.SetActive(true);

        if(InterActionStick != null)
        {
            Destroy(InterActionStick);
        }
    }


}
