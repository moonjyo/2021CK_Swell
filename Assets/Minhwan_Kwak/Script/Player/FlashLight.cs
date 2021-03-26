using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

   public GameObject Flash;
    public float AngleSpeed = 0f;
   public Vector2 AngleMinMax;
   

   private bool IsToggle = false;

   private Vector2 AngleVec;
    public void SetAngleValue(Vector2 value)
    {
        AngleVec = value;
    }

    private void Update()
    {
        if(AngleVec.sqrMagnitude > 0.1f)
        {//down
            if(AngleVec.y == 1)
            {
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z) * Time.deltaTime * AngleSpeed);
               
                if(transform.eulerAngles.z >= AngleMinMax.x)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, AngleMinMax.x);
                }
                
            }
            else
            {
           //up
                transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z) * Time.deltaTime * -AngleSpeed);

                if (transform.eulerAngles.z <= AngleMinMax.y)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, AngleMinMax.y);
                }
            }

        }
        
    }

    public void Toggle()
    {
        IsToggle = !IsToggle;
        if(IsToggle)
        {
            FlashOn();
        }
        else
        {
            FlashOff();
        }
    }


    public void FlashOff()
    {
        Flash.SetActive(false);
    }
    public void FlashOn()
    {
        Flash.SetActive(true);
    }
}
