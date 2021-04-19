using UnityEngine;

public interface IInteractableUI
{
     void Interact();

     GameObject GetTargetObj();

     Canvas GetParentCanvas();

     void SetTargetCanvas(Canvas targetobj);

     void SetTargetObj(GameObject targetobj);

     void Init();

    Transform GetTransform();
    
  
}

