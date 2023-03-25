using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Animator animator;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator.SetBool("IsIdle", true);
    }

    private void FixedUpdate()
    {
        //Gets input from WSAD
        Vector3 Delta = new Vector3(Input.GetAxis("Horizontal") * 3, 0, Input.GetAxis("Vertical") * 3);

        //Gets speed of player
        float Speed = rb.velocity.magnitude;

        //Makes sure that the speed is greater than 0
        float DeltaSpeed = Mathf.Max(5.0f - Speed, 0.0f);

        //Applies the speed
        rb.AddRelativeForce(Delta * DeltaSpeed);
        // Get the player's velocity along the z-axis (forward/backward)
        float zVelocity = rb.velocity.z;

        // Set the animation trigger speed based on the player's velocity
        if (zVelocity > 0)
        {
            animator.SetFloat("Speed", zVelocity);
        }
        else if (zVelocity < 0)
        {
            animator.SetFloat("Speed", -zVelocity);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (Speed > 0)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsIdle", true);
        }
    }
}
