using UnityEngine;
using System.Collections;

public class Platforms : MonoBehaviour {

	public float Speed = 0.1f;
	public GameObject prefab;
	public GameObject paizinho;
	bool cooldown = false;

	void Update () {

	    transform.Translate(Vector2.down * Time.deltaTime * Speed);
		if (!cooldown) {
			StartCoroutine (SPAWN ());
		}
	}

	IEnumerator SPAWN()
	{   
		cooldown = true;
		yield return new WaitForSeconds (5f);
		GameObject instance = (GameObject) Instantiate (prefab,transform.SetParent(paizinho));
		//prefab.transform.SetParent (paizinho);
		cooldown = false;
	}
}
