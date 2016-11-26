using UnityEngine;
using System.Collections;

// Creates platforms for generation.
public class PlatformFactory : MonoBehaviour {

    [Header("Spawning Properties")]
	public float speed = 0.1f;
    public Vector2 spawnPosition;
	public GameObject prefab;

    // Internal.
    private bool _coolDown = false;
    private WaitForSeconds _waitTimer;

    // Use this for initialization
    void Start()
    {
        _waitTimer = new WaitForSeconds(5f);
    }

    // Update is called every frame
	void Update () {

        // Create the next set of platforms.
		if (!_coolDown) {
			StartCoroutine (CreateNext ());
		}
	}

    // Creates the next set of platforms.
	IEnumerator CreateNext()
	{   
		_coolDown = true;
        yield return _waitTimer;
        currentObject = (GameObject) Instantiate (prefab, spawnPosition, Quaternion.identity);
		_coolDown = false;
	}
}
