using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour
{

    public FMODUnity.StudioEventEmitter fireplaceEmitter;


    private void OnTriggerStay(Collider other)
    {
        fireplaceEmitter.Stop();
    }
    private void OnTriggerExit(Collider other)
    {
        fireplaceEmitter.Play();
    }
}
