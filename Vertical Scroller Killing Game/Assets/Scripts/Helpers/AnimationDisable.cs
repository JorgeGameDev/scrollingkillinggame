using UnityEngine;
using System.Collections;

// Used to disable objects at the end of animations.
public class AnimationDisable : MonoBehaviour {

    // Disables the animation.
	public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
