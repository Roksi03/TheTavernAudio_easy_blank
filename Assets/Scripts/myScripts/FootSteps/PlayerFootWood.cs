using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using FMODUnity;

public class PlayerFootWood : State
{

    FMOD.Studio.EventInstance FootstepsSound;
    private string footstepsEvent = "event:/Sfx/footsteps";






    private CharacterController controller;
   
   
    private float speed = 5f;
    Vector3 velocity;
    public float gravity = -9.81f * 2;
    private float lastFootstepTime = 0f;
    private float distToGround;
    public PlayerFootWood(StateMachine.StateMachine stateMachine) : base(stateMachine) { }


    

    public override void Enter()
    {
        controller =stateMachine.GetComponent<CharacterController>();
      
        distToGround = stateMachine.GetComponent<Collider>().bounds.extents.y;
    }
    public override void Update()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = stateMachine.transform.right * x + stateMachine.transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (IsGrounded() && Time.time - lastFootstepTime > 0.5f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();

            }
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

    bool IsGrounded()
    {
        return Physics.Raycast(stateMachine.transform.position, Vector3.down, distToGround + 0.5f);
    }
}
