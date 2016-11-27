using UnityEngine;
using System.Collections.Generic;

// Controls the animation of the weapon.
[RequireComponent(typeof(BoxCollider2D))]
public class WeaponController : MonoBehaviour {

    [Header("Animation Style")]
    [ReadOnly]
    public bool animationPlaying;

    // Internal Components.
    private List<GameObject> _hitGameObjects = new List<GameObject>();
    private ClassInfo _classInfo;
    private BoxCollider2D _boxCollider;
    private Animator _animator;

    // Use this for initialization.
    void Start()
    {
        _classInfo = transform.parent.GetComponent<ClassInfo>();
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
        _hitGameObjects.Clear();
        animationPlaying = false;
        _boxCollider.enabled = false;
    }

    // Checks if the weapon has hit any other player.
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Confirms that the attacked object is infact a player and not something else.
        if(collision.gameObject.CompareTag("Player"))
        {
            // Stores this game object to quicker acess.
            GameObject collidedPlayer = collision.gameObject;

            // Checks if this player hasn't already been collided during this hit session.
            if(!_hitGameObjects.Contains(collidedPlayer))
            {
                _hitGameObjects.Add(collidedPlayer);
                collidedPlayer.GetComponent<PlayerDamage>().ApplyDamage(_classInfo.attackDamage, _classInfo.knockbackForce);
            }
        }
    }
}
