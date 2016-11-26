using UnityEngine;
using System.Collections;

// Does the movement of the platforms.
public class PlatformMovement : MonoBehaviour {

    [Header("Platform Properties")]
    public float speed;

	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }
}
