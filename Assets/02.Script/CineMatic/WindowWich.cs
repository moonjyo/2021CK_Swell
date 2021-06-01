using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWich : MonoBehaviour
{
    public Material WindowMaterial;

    private void Start()
    {
        OffWichStart();
    }

    public  void OnWichStart()
    {
        PlayerManager.Instance.playerAnimationEvents.PlayerAnim.SetInteger(PlayerAnimationEvents.State, (int)AnimState.CANCEL);
        WindowMaterial.SetColor("_BaseColor", Color.red);
        WindowMaterial.SetColor("_EmissionColor", Color.red);
    }

    public void OffWichStart()
    {
        WindowMaterial.SetColor("_BaseColor", new Color(1f,1f,1f));
        WindowMaterial.SetColor("_EmissionColor", new Color(3.688606f, 1.969831f, 1.158724f));
        GameManager.Instance.eventCommand.EventsTriggerList[(int)EventTriggerEnum.FLASHLIGHT].gameObject.GetComponent<Animator>().SetTrigger("FlashLight");

    }




}
