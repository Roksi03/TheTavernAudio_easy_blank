using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using FMODUnity;


public class PlayerJump : State
{
    FMOD.Studio.EventInstance JumpSound;
    private string jumpEvent = "event:/Sfx/jump";

    float jumpHeight = 1f;
    private float distToGround;
   

    private MovementManager movementManager;
    private PlayerFoot playerFoot;

    public static PlayerJump playerJump;
    public PlayerJump(StateMachine.StateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        movementManager = stateMachine.GetComponent<MovementManager>();
        distToGround = stateMachine.GetComponent<Collider>().bounds.extents.y;

        JumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
        movementManager.Jump(jumpHeight);
    }

    public override void Update()
    {

        Vector3 move = movementManager.Walk();
        movementManager.Move(move,4f);
        movementManager.Gravity();
        PlayJump();

        if (movementManager.IsGrounded() && movementManager.velocity.y <= 0)
        {
            stateMachine.Begin(new PlayerFoot(stateMachine));
        }
    }

    public void PlayJump()
    {

        RaycastHit hit;
       

        if (Physics.Raycast(stateMachine.transform.position, Vector3.down, out hit, distToGround + 0.5f))
        {
           
            if (hit.collider.CompareTag("Wood") || hit.collider.CompareTag("Inside_wood"))
            {
                JumpSound.setParameterByNameWithLabel("footSwitcher", "wood");
                JumpSound.start();

                if (movementManager.IsGrounded())
                {
                    JumpSound.setParameterByNameWithLabel("footSwitcher", "wood");
                    JumpSound.setParameterByNameWithLabel("isGrounded", "false");
                    JumpSound.start();
                }
            }
            else if (hit.collider.CompareTag("Stone") || hit.collider.CompareTag("Inside_stone"))
            {
                JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                JumpSound.start();

                if (movementManager.IsGrounded())
                {
                    JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                    JumpSound.setParameterByNameWithLabel("isGrounded", "false");
                    JumpSound.start();
                }
            }
            else
            {
                JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                JumpSound.start();


                 if (movementManager.IsGrounded())
                {
                    JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                    JumpSound.setParameterByNameWithLabel("isGrounded", "false");
                    JumpSound.start();
                }
            }
           
           
          
        }

       
    }
}
