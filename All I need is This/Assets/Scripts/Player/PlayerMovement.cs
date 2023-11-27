using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public Rigidbody2D rigidbody;
    public Animator animator;

    Vector2 movement;
    
    // for REGISTERING INPUT
    void Update()
    {
        // Input

        // left is -1, right is 1, no action is 0
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // animations getting animated
        animator.SetFloat("speedVert", movement.y);
        animator.SetFloat("speedHoriz", movement.x);
    }

    // USE THIS for PHYSICS related stuff
    // because this uses a set timer and
    // does not change if frame rate changes
    void FixedUpdate()
    {
        // Movement
        rigidbody.MovePosition(rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
