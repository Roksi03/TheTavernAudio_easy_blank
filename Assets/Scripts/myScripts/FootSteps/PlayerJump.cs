using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;


public class PlayerJump : State
{

    float jumpHeight = 1f;

    private MovementManager movementManager;
    private PlayerFoot playerFoot;
   

    public PlayerJump(StateMachine.StateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
       movementManager = stateMachine.GetComponent<MovementManager>();
       

        movementManager.Jump(jumpHeight);
    }

    public override void Update()
    {

        Vector3 move = movementManager.Walk();
        movementManager.Move(move);
        movementManager.Gravity();

        if (movementManager.IsGrounded() && movementManager.velocity.y <= 0)
        {
            stateMachine.Begin(new PlayerFoot(stateMachine));
        }
    }
}
