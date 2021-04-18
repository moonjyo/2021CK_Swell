using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInterAction : MonoBehaviour, IInteractbale
{
    [SerializeField] private List<GameObject> InterActableGameObjects;



    public void Interact()
    {
        foreach(var InterActableGameObject in InterActableGameObjects)
        {
            var Interactable = InterActableGameObject.GetComponent<IInteractbale>();
            if (Interactable != null) continue;
            Interactable.Interact();
        }
    }
}
