using UnityEngine;
using System.Collections;

// Creates platforms for generation.
public class PlatformFactory : MonoBehaviour {

    [Header("Spawning Properties")]
	public GameObject[] layouts;
	public float speed = 0.1f;
    public Vector2 spawnPosition;
	public GameObject firstplatform;
	public GameObject player;

    // Internal.
    private bool _coolDown = false;
    private WaitForSeconds _waitTimer;

    // Use this for initialization
    void Start()
    {
        _waitTimer = new WaitForSeconds(20f);
    }

    // Update is called every frame
	void Update () {

        // Create the next set of platforms.
		if (!_coolDown) {
			StartCoroutine (CreateNext ());
		}
		// Destroy the first platform.
		Destroy (firstplatform, 31f);

	}

    // Creates the next set of platforms from a pool of layouts and deletes the previous cloned layout.
	IEnumerator CreateNext()
	{   
		_coolDown = true;
        yield return _waitTimer;
		GameObject newObject = (GameObject) Instantiate (layouts[Random.Range(0,layouts.Length)], spawnPosition, Quaternion.identity);
		Destroy (newObject, 31f);
		_coolDown = false;
	}
		
}
