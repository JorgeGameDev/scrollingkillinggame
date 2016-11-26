﻿using UnityEngine;
using System.Collections;

// Takes care of the player movement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Header("Jumping Properties")]
    public Transform jumpingCheck;
    public LayerMask groundLayer;

    // Internal.
    private Rigidbody2D _rigidbody;
    private ClassInfo _classInfo;

	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _classInfo = GetComponent<ClassInfo>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Calls all the necessary functions for player movement.
        HorizontalMovement();

        // Checks if the player has pressed the jump key.
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _rigidbody.AddForce(_classInfo.jumpForce * 10 * Vector2.up);
        }
    }

    // Does the horizontal movement.
    void HorizontalMovement()
    {
        // Gets the axis from the player controller.
        float horAxis = Input.GetAxisRaw("Horizontal");

        // Applies the velocity to the characther based on the player input.
        Vector2 newVelocity = new Vector2(horAxis * _classInfo.velocity, _rigidbody.velocity.y);
        _rigidbody.velocity = newVelocity;
    }

    // Checks if the player is still touching the grand.
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(jumpingCheck.transform.position, 0.01f, groundLayer);
    }
}
