using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using FMODUnity;
using UnityEngine.LowLevel;

public class PlayerFoot : State
{

    



    FMOD.Studio.EventInstance FootstepsSound;
    private string footstepsEvent = "event:/Sfx/footsteps";






    
   
   
  
   private MovementManager movementManager;
    private float lastFootstepTime = 0f;
    private float distToGround;

   
    public PlayerFoot(StateMachine.StateMachine stateMachine) : base(stateMachine) { }


    

    public override void Enter()
    {
       
       movementManager = stateMachine.GetComponent<MovementManager>();
        distToGround = stateMachine.GetComponent<Collider>().bounds.extents.y;
    }
    public override void Update()
    {
        if (movementManager.IsGrounded())
        {
           movementManager.ResetVelocity();
        }


       Vector3 move = movementManager.Walk();

        movementManager.Move(move);
        movementManager.Gravity();

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (movementManager.IsGrounded() && Time.time - lastFootstepTime > 0.5f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();

            }
        }
        if (Input.GetButtonDown("Jump") && movementManager.IsGrounded())
        {
            
            stateMachine.SetState(new PlayerJump(stateMachine));

        }
    }

     private void PlayFootsteps()
    {
        RaycastHit hit;

        if (Physics.Raycast(stateMachine.transform.position, Vector3.down, out hit, distToGround + 0.5f))
        {
            if (hit.collider.CompareTag("Wood") || hit.collider.CompareTag("Inside_wood"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(stateMachine.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "wood");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Stone") || hit.collider.CompareTag("Inside_stone"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(stateMachine.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(stateMachine.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            
               

            
                
        }
    }

  
}
