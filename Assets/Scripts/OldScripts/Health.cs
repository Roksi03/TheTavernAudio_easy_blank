using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Health : MonoBehaviour
{
    
    private bool health = false;

    FMOD.Studio.EventInstance HealthSnap;

    public EventReference healthSnapshot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!health)
            {
                HealthSnap = FMODUnity.RuntimeManager.CreateInstance(healthSnapshot);
                HealthSnap.start();
                health = !health;
            }
            else if (  health)
            {
                HealthSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                HealthSnap.release();
                health = !health;
            }
            
        }
    }
}