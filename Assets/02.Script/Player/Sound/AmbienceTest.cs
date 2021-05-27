using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceTest : MonoBehaviour
{
    public string BirdSounds;
    FMOD.Studio.EventInstance BirdSoundsEvent;

    private void Start()
    {
        BirdSoundsEvent = FMODUnity.RuntimeManager.CreateInstance(BirdSounds);
        BirdSoundsEvent.start();
    }

    private void Update()
    {
      FMODUnity.RuntimeManager.AttachInstanceToGameObject(BirdSoundsEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
