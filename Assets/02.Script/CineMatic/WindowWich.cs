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
        WindowMaterial.SetColor("_BaseColor", new Color(5.4679f,0.7f,0.7f));
        WindowMaterial.SetColor("_EmissionColor", new Color(4f, 4f, 4f));
    }




}
