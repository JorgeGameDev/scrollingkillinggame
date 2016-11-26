using UnityEngine;
using System.Collections;

// Controls the animation of the weapon.
[RequireComponent(typeof(BoxCollider2D))]
public class WeaponController : MonoBehaviour {

    [Header("Animation Style")]
    [ReadOnly]
    public bool animationPlaying;

    // Internal Components.
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    // Use this for initialization.
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }
   
	// Use this to control the start of the animation.
	public void StartAnimation()
    {
        animationPlaying = true;
        _boxCollider.enabled = true;
        _animator.SetTrigger("Attack");
    }

    // Use this to check if the animation is over.
    public void EndAnimation()
    {
        animationPlaying = false;
        _boxCollider.enabled = false;
    }
}
