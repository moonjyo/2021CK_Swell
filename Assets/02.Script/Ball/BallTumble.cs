using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTumble : MonoBehaviour
{
    Rigidbody rigidBody;
    Vector3 movement;
    float fTime = 0.0f;
    bool IsTumbleEnd = false;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        movement = transform.forward;
        gameObject.layer = 0;
    }

    private void FixedUpdate()
    {
        fTime += Time.fixedDeltaTime;
        if (fTime < 3.5f)
        {
            rigidBody.AddForce(movement * 10.0f);
        }
        else
        {
            IsTumbleEnd = true;
            gameObject.layer = 17;
        }
            


    }
}
