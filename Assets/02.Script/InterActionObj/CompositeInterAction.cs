using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeInterAction : MonoBehaviour, IInteractbale
{
    [SerializeField] private List<GameObject> InterActableGameObjects;

    public bool IsInterAction = false;
    public List<GameObject> GetUIObjList()
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


    public void SecondInteractOff()
    {
        throw new System.NotImplementedException();
    }

    public void SecondInteractOn()
    {
        throw new System.NotImplementedException();
    }

    public bool IsGetInterAction()
    {
        return IsInterAction;
    }
}
