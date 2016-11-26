using UnityEngine;
using System.Collections;

// Creates platforms for generation.
public class Platforms : MonoBehaviour {

    [Header("Spawning Properties")]
	public float Speed = 0.1f;
	public GameObject prefab;
	public Transform paizinho;

    // Internal.
    private bool _coolDown = false;

    // Update is called every frame
	void Update () {
        // Translates this platform.
	    transform.Translate(Vector2.down * Time.deltaTime * Speed);
		if (!_coolDown) {
			StartCoroutine (CreateNext ());
		}
	}

    // Creates the next set of platforms.
	IEnumerator CreateNext()
	{   
		_coolDown = true;
		yield return new WaitForSeconds (5f);
		GameObject instance = (GameObject) Instantiate (prefab, transform.position, Quaternion.identity));
		prefab.transform.SetParent (paizinho);
		_coolDown = false;
	}
}
