using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class PlayerFootWood : State
{
    private CharacterController controller;
   
    private Rigidbody rb;
    private float speed = 8f;

    private float lastFootstepTime = 0f;
    private float distToGround;
    public PlayerFootWood(StateMachine.StateMachine stateMachine) : base(stateMachine) { }


    

    public override void Enter()
    {
        controller =stateMachine.GetComponent<CharacterController>();
        rb = stateMachine.GetComponent<Rigidbody>();
        distToGround = stateMachine.GetComponent<Collider>().bounds.extents.y;
    }
    public override void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = stateMachine.transform.right * x + stateMachine.transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (IsGrounded() && Time.time - lastFootstepTime > 0.5f)
            {
                lastFootstepTime = Time.time;
               
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(stateMachine.transform.position, Vector3.down, distToGround + 0.5f);
    }
}
