using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Manages the global variables of the UI.
public class GameManager : MonoBehaviour {

    // Manages all the players.
    [Header("Player Manager")]
    public GameObject[] players;

    [Header("UI Elements")]
    public GameObject playerText;
    public RectTransform playerRect;

    // Internal.
    public int _currentController;

	// Use this for initialization
	void Start () {
	    // Creates a new player text for each player.
        foreach(GameObject player in players)
        {
            // Creates a player at an assigned spawn point.
            _currentController++;
            Vector3 spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), 5f, 0f);
            GameObject newPlayer = (GameObject)Instantiate(player, spawnPosition, Quaternion.identity);

            // Creates a new UI with the percentages.
            GameObject createdUi = (GameObject)Instantiate(playerText, transform.position, Quaternion.identity);
            createdUi.transform.SetParent(playerRect);

            // Updates the text to reflect the player.
            ClassInfo classInfo = newPlayer.GetComponent<ClassInfo>();
            createdUi.transform.FindChild("Player Text").GetComponent<Text>().text = classInfo.className;
            newPlayer.GetComponent<PlayerDamage>().associatedCounter = createdUi.transform.FindChild("Player Damage").GetComponent<Text>();

            // Assigns the controler to this characther.
            PlayerController playerControler = newPlayer.GetComponent<PlayerController>();
            playerControler.playerController = _currentController;
        }
	}
}
