using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
     public Vector3 velocity;

    private float gravity = -9.81f * 2;
    private float speed = 5f;

    private CharacterController controller;

    private void Awake()
    {
       controller = GetComponent<CharacterController>();
    }

    public void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity *  Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    public void ResetVelocity()
    {
        if (velocity.y < 0)
            velocity.y = -2f;
    }

    public void Jump(float jumpHeight)
    {
        velocity.y =Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    public bool IsGrounded()
    {
        return GetComponent<CharacterController>().isGrounded;
    }
    public Vector3 Walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x +transform.forward * z;
        return move;
    }
}
