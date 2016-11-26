using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

// Takes care of the player movement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    [Header("Jumping Properties")]
    public Transform jumpingCheck;
    public LayerMask groundLayer;

    // Internal.
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private ClassInfo _classInfo;
    private PlayerDamage _playerDamage;
    private bool _hasJumped;

	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _classInfo = GetComponent<ClassInfo>();
        _playerDamage = GetComponent<PlayerDamage>();
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Calls all the necessary functions for player movement.
        HorizontalMovement();
        Jumping();
        Animate();
    }

    // Updates the animator to reflect the current animation playing.
    void Animate()
    {
        if(_rigidbody.velocity.x > 0 || _rigidbody.velocity.x < 0)
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    // Does the horizontal movement.
    void HorizontalMovement()
    {
        // Gets the axis from the player controller.
        float horAxis = 0;

        // Checks if the player has pressed any specific key.
        if(Input.GetKey(_classInfo.moveRight) || XCI.GetButton(XboxButton.DPadRight, _classInfo.assignedController))
        {
            horAxis = 1;
        }
        else if(Input.GetKey(_classInfo.moveLeft) || XCI.GetButton(XboxButton.DPadLeft, _classInfo.assignedController))
        {
            horAxis = -1;
        }

        // Applies the velocity to the characther based on the player input.
        float horVelocity = horAxis * _classInfo.velocity;

        // Causes a 
        if(horVelocity > 0)
        {
            horVelocity -= _playerDamage.playerDamage / 100;
        }
        else if(horVelocity < 0)
        {
            horVelocity += _playerDamage.playerDamage / 100;
        }

        Vector2 newVelocity = new Vector2(horVelocity, _rigidbody.velocity.y);
        _rigidbody.velocity = newVelocity;

        // Check which direction to flip the sprite on.
        if(horAxis != 0)
        {
            transform.localScale = new Vector3(horAxis, 1, 1);
        }
    }

    // Makes the player jump. No shit, sherlock I can't think of a more obvious definition.
    void Jumping()
    {
        // Checks if the player has pressed the jump key.
        if ((Input.GetKeyDown(_classInfo.jumpKey) || XCI.GetButtonDown(XboxButton.A, _classInfo.assignedController)) && IsGrounded())
        {
            _hasJumped = true;
            _rigidbody.AddForce(_classInfo.jumpForce * 10 * Vector2.up);
        }

        // Checks if the player's velocity has been zero at some point.
        if(_rigidbody.velocity.y == 0)
        {
            _hasJumped = false;
        }
    }

    // Checks if the player is still touching the grand.
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(jumpingCheck.transform.position, 0.01f, groundLayer);
    }


    // Assures the player is on a platform.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }

    // And removes it's parent in case it's null.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            transform.SetParent(null);
        }
    }
}
