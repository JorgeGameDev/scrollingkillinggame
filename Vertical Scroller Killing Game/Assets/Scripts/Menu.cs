using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {


	void Update () 
	{
		if ( Input.GetButtonDown ("Joystick 1 Jump") ) 
		{
				SceneManager.LoadScene ("Game 2");
		}

		if ( Input.GetButtonDown ("Joystick 1 Attack") ) 
		{
			Application.Quit();
		}
	}
}
