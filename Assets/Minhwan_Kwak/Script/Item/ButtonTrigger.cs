using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : ItemBase
{
   public Transform TriggerOnObj;



    private void OnTriggerEnter(Collider other)
    {
        if(CompareTag(other.tag))
        {
          Animator anim = TriggerOnObj.GetComponent<Animator>();
            anim.SetTrigger("TestTrigger");
        }
    }
}
