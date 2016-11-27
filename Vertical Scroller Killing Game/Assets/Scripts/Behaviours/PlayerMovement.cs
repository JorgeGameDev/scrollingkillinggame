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
    private ClassInfo _classInfo;
    private PlayerController _playerController;
    private PlayerDamage _playerDamage;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private WaitForSeconds _knockbackTime;
    private AudioSource _audioSource;
    private bool _hasJumped;
    private bool _isKnockbacked;

	// Use this for initialization
	void Start ()
    {
        _playerController = GetComponent<PlayerController>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _classInfo = GetComponent<ClassInfo>();
        _playerDamage = GetComponent<PlayerDamage>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        // Calls all the necessary functions for player movement.
        if(!_isKnockbacked)
        {
            HorizontalMovement();
            Jumping();
        }

        Animate();
        ControlPosition();
    }

    // Does the horizontal movement.
    void HorizontalMovement()
    {
        // Gets the axis from the player controller.
        float horAxis = Input.GetAxisRaw("Joystick " + _playerController.playerController + " Horizontal");
        horAxis = Mathf.Round(horAxis);

        // Checks if the player has pressed any specific key.
        /*if(Input.GetKey(_classInfo.moveRight) || XCI.GetButton(XboxButton.DPadRight, _classInfo.assignedController))
        {
            horAxis = 1;
        }
        else if(Input.GetKey(_classInfo.moveLeft) || XCI.GetButton(XboxButton.DPadLeft, _classInfo.assignedController))
        {
            horAxis = -1;
        }*/

        // Applies the velocity to the characther based on the player input.
        float horVelocity = horAxis * _classInfo.velocity;

        // Reduces velocity based on the player's damage.
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
        if (Input.GetButtonDown("Joystick " + _playerController.playerController + " Jump") && IsGrounded() && !_hasJumped)
        {
            _hasJumped = true;
            _audioSource.Play();
            _rigidbody.AddForce(_classInfo.jumpForce * 10 * Vector2.up);
        }

        // Checks if the player's velocity has been zero at some point.
        if(_rigidbody.velocity.y == 0)
        {
            _hasJumped = false;
        }
    }

    // Updates the animator to reflect the current animation playing.
    void Animate()
    {
        if (_rigidbody.velocity.x > 0 || _rigidbody.velocity.x < 0)
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    // Controls the player position.
    void ControlPosition()
    {
        // Makes sure that the player position doesn't go off secreen.
        Vector3 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, -6.7f, 6.7f);

        // Checks if the player hasn't died.
        if(playerPosition.y < -4.6f && !_playerDamage.isRespawning)
        {
            // Kills the player to respawn them.
            _rigidbody.isKinematic = true;
            GetComponent<BoxCollider2D>().enabled = false;
            _playerDamage.KillPlayer();
        }

        // Sets the player position.
        transform.position = playerPosition;
    }

    // Checks if the player is still touching the grand.
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(jumpingCheck.transform.position, 0.1f, groundLayer);
    }

    // Used to respawn the player.
    public void RespawnPlayerPos()
    {
        // Resets rigidbody and player position.
        _rigidbody.isKinematic = false;
        GetComponent<BoxCollider2D>().enabled = true;
        Vector3 spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
        transform.position = spawnPosition;
    }

    // Does knockback to the player.
    public void ReceiveKnockback(float knockbackForce)
    {
        StartKnockback(knockbackForce);
    }

    IEnumerator StartKnockback(float knockbackForce)
    {
        // Gives the knock back to the player.
        _isKnockbacked = true;
        Vector2 knockbackVector = new Vector2(knockbackForce, knockbackForce / 2);

        // Applies the force vector of the knockback.
        if (transform.localScale.x == 1)
        {
            knockbackVector.x *= 1;
        }

        // Adds force to the rigidbody.
        _rigidbody.AddForce(knockbackVector);
        yield return new WaitForSeconds(0.5f);
        _isKnockbacked = false;
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
