using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInterAction : MonoBehaviour, IInteractbale
{
    [SerializeField] private List<GameObject> InterActableGameObjects;

    public void FirstInteract()
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetUIFirstList()
    {
        throw new System.NotImplementedException();
    }

    public List<GameObject> GetUISecondList()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        foreach(var InterActableGameObject in InterActableGameObjects)
        {
            var Interactable = InterActableGameObject.GetComponent<IInteractbale>();
            if (Interactable != null) continue;
            //Interactable.Interact();
        }
    }

    public void SecondInteract()
    {
        throw new System.NotImplementedException();
    }
}
