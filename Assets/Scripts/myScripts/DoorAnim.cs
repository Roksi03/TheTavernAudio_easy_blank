using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DoorAnim : MonoBehaviour,IInteractable
{

    [SerializeField] Animator animator;

    private bool OpenDoor = true;
    private bool isRotating = false;


    FMOD.Studio.EventInstance DoorsSound;
    public EventReference DoorsEvent;

    //FMOD.Studio.EventInstance InsideRoom;
   // public EventReference insideRoomSnap;

    public void Interact()
    {
        if (!isRotating)
        {
           DoorsInteract();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

   void DoorClose()
    {
        Debug.Log("close");
        isRotating = true;
        animator.SetBool("isClose", true);

    }
    void DoorOpen()
    {
        isRotating = true ;
        animator.SetBool("isClose",false);
        
    }

     public void CloseSound1()
    {
        Debug.Log("closesound");
        
        
            Debug.Log("Animator Event triggered: CloseSound1");
            DoorsSound = FMODUnity.RuntimeManager.CreateInstance(DoorsEvent);
            DoorsSound.setParameterByNameWithLabel("doorSwitcher", "close");
            DoorsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            DoorsSound.start();
        
    }
   public  void CloseSound2()
    {
        Debug.Log("closesound3");
        
        
            DoorsSound = FMODUnity.RuntimeManager.CreateInstance(DoorsEvent);
            DoorsSound.setParameterByNameWithLabel("doorSwitcher", "close2");
            DoorsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
            DoorsSound.start();
        
    }

    public void OpenSound()
    {
        DoorsSound = FMODUnity.RuntimeManager.CreateInstance(DoorsEvent);
        DoorsSound.setParameterByNameWithLabel("doorSwitcher", "open");
        DoorsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        DoorsSound.start();
    }




    void DoorsInteract()
    {
        if(OpenDoor == true)
        {
            DoorClose();
            
           

            OpenDoor = false;
            isRotating=false;
           
        }
        else
        {
            DoorOpen();
           
            OpenDoor = true;
            isRotating = false;
        
        }
    }

    
}
