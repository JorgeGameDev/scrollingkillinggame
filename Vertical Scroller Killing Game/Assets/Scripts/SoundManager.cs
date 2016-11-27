using UnityEngine;
using System.Collections;

// Used for playing sounds.
public class SoundManager : MonoBehaviour {

    public static SoundManager soundManager;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
    {
	    // Assures this is the only sound manager in existence.
        if(soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    // Update is called every frame.
    void Update()
    {

    }
}
