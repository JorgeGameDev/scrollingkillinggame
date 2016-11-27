using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Used for menu functionality.
public class Menu : MonoBehaviour {

    // Update is called every frame
	void Update () 
	{
        // Checks for input loading the actual game scene.
		if (Input.GetButtonDown ("Joystick 1 Jump") || Input.GetButtonDown("Joystick 2 Jump") || Input.GetButtonDown("Joystick 3 Jump") || Input.GetButtonDown("Joystick 4 Jump")) 
		{
			SceneManager.LoadScene (1);
		}

        // Checks for input on quit.
		if (Input.GetButtonDown ("Joystick 1 Attack") || Input.GetButtonDown("Joystick 2 Attack") || Input.GetButtonDown("Joystick 3 Attack") || Input.GetButtonDown("Joystick 4 Attack")) 
		{
			Application.Quit();
		}
	}
}