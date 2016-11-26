using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

// Takes care of the player movement.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

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
	void Update () {

        // Gets the axis from the player controler.
        float horAxis = XCI.GetAxisRaw(XboxAxis.LeftStickX);
        if(XCI.GetButtonDown(XboxButton.DPadRight))
        {
            horAxis = 1;
        }
        else if(XCI.GetButtonDown(XboxButton.DPadLeft))
        {
            horAxis = -1;
        }

        // Applies the velocity to the characther based on the player input.
        Vector2 newVelocity = new Vector2(horAxis * _classInfo.velocity, _rigidbody.velocity.y);
        _rigidbody.velocity = newVelocity;
	}
}
