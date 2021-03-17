using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : ItemBase
{
   public Rigidbody rb;
   public delegate void ItemStatusFunc();
   
  
   public ItemStatusFunc StatusFunc;


    private void Start()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
    }


}
