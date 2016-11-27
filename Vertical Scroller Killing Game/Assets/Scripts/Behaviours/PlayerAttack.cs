using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

// Takes care of the player attacking.
public class PlayerAttack : MonoBehaviour {

    // Internal.
    private PlayerController _playerController;
    private WeaponController _weaponController;
    private ClassInfo _classInfo;

	// Use this for initialization
	void Start ()
    {
        _playerController = GetComponent<PlayerController>();
        _classInfo = GetComponent<ClassInfo>();
        _weaponController = GetComponentInChildren<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
	    // Checks if the player has pressed it's assigned controls.
        if(Input.GetButtonDown("Joystick " + _playerController.playerController + " Attack"))
        {
            // Checks if the animation is already playing or not.
            if(!_weaponController.animationPlaying)
            {
                // Plays the attack animation.
                _weaponController.StartAnimation();
            }
        }
	}
}
