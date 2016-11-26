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

	// Use this for initialization
	void Start () {
	    // Creates a new player text for each player.
        foreach(GameObject player in players)
        {
            // Creates a player at an assigned spawn point.
            Instantiate(player, Vector3.zero, Quaternion.identity);

            // Creates a new UI with the percentages.
            GameObject createdUi = (GameObject)Instantiate(playerText, transform.position, Quaternion.identity);
            createdUi.transform.SetParent(playerRect);

            // Updates the text to reflect the player.
            ClassInfo classInfo = player.GetComponent<ClassInfo>();
            createdUi.transform.FindChild("Player Text").GetComponent<Text>().text = classInfo.className;
            player.GetComponent<PlayerDamage>().associatedCounter = createdUi.transform.FindChild("Player Damage").GetComponent<Text>();
        }
	}
}
