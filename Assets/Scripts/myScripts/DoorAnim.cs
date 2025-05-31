using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class DoorAnim : MonoBehaviour,IInteractable
{

    [SerializeField] Animator animator;

    private bool OpenDoor = true;
    private bool isRotating = false;

    


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
