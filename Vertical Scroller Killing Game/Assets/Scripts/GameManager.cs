using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// Manages the global variables of the UI.
public class GameManager : MonoBehaviour {

    // Manages all the players.
    public static GameManager gameManager;

    [Header("Game Flow")]
    public int respawnTime;
    public GameObject fleshBase;
    public Sprite[] fleshSprites;
    public Sprite[] lavaSprites;

    [Header("Player Manager")]
    public GameObject[] players;
    public int playersAlive;

    [Header("Audio")]
    public AudioClip damageClip;
    public AudioClip deathClip;

    [Header("UI Elements")]
    public GameObject playerText;
    public RectTransform playerRect;
    public GameObject gameOverCanvas;
    public Text winnerText;

    // Internal.
    public int _currentController;

    // Use this for early initialization.
    void Awake()
    {
        gameManager = this;
    }

    // Use this for initialization
    void Start () {
	    // Creates a new player text for each player.
        foreach(GameObject player in players)
        {
            // Creates a player at an assigned spawn point.
            _currentController++;
            Vector3 spawnPosition = new Vector3(Random.Range(-3.5f, 3.5f), 5f, 0f);
            GameObject newPlayer = (GameObject)Instantiate(player, spawnPosition, Quaternion.identity);

            // Creates a new UI with the percentages.
            GameObject createdUi = (GameObject)Instantiate(playerText, transform.position, Quaternion.identity);
            createdUi.transform.SetParent(playerRect);
            createdUi.transform.localScale = Vector3.one;

            // Updates the text to reflect the player.
            ClassInfo classInfo = newPlayer.GetComponent<ClassInfo>();
            createdUi.transform.FindChild("Player Text").GetComponent<Text>().text = classInfo.className;
            newPlayer.GetComponent<PlayerDamage>().associatedCounter = createdUi;

            // Assigns the controler to this characther.
            PlayerController playerControler = newPlayer.GetComponent<PlayerController>();
            playerControler.playerController = _currentController;
        }

        // Counts players alive to the count.
        playersAlive = players.Length;
    }

    // Used for creating gore at a certain position.
    public void CreateGore(Vector3 transformPosition, int goreAmmount)
    {
        // Creates a new piece of gore at a given position.
        for(int i = 0; i < goreAmmount; i++)
        {
            // Creates the gore.
            Sprite fleshSprite = fleshSprites[Random.Range(0, fleshSprites.Length)];
            GameObject newFlesh = (GameObject)Instantiate(fleshBase, transformPosition, Quaternion.identity);

            // Assigns the variables for the gore.
            newFlesh.GetComponent<SpriteRenderer>().sprite = fleshSprite;
            Vector2 forceVector = new Vector2(Random.Range(-200f, 200f), Random.Range(200f, 300f));
            newFlesh.GetComponent<Rigidbody2D>().AddForce(forceVector);

            // Sets the object to be destroyed after a while.
            Destroy(newFlesh, 2f);
        }
    }

    // Used for creating lava spikes at a certain position.
    public void CreateLava(Vector3 transformPosition)
    {
        // Creates a new piece of gore at a given position.
        for (int i = 0; i < 20; i++)
        {
            // Creates the gore.
            Sprite lavaSprite = lavaSprites[Random.Range(0, lavaSprites.Length)];
            GameObject newLava = (GameObject)Instantiate(fleshBase, transformPosition, Quaternion.identity);

            // Assigns the variables for the gore.
            newLava.GetComponent<SpriteRenderer>().sprite = lavaSprite;
            Vector2 forceVector = new Vector2(Random.Range(-100f, 100f), Random.Range(400f, 600f));
            newLava.GetComponent<Rigidbody2D>().AddForce(forceVector);

            // Sets the object to be destroyed after a while.
            Destroy(newLava, 4f);
        }
    }

    // Keeps track of the players alive.
    public void KillPlayer()
    {
        playersAlive--;
        string playerAlive = "???";
        // Checks which player is still alive.
        if(playersAlive == 1)
        {
            // Checks which of the characthers is not dead.
            foreach(GameObject player in players)
            {
                if(!player.GetComponent<PlayerDamage>().isDead)
                {
                    playerAlive = player.GetComponent<ClassInfo>().className;
                }
            }

            // Updates the canvas.
            gameOverCanvas.SetActive(true);
            winnerText.text = playerAlive + " Wins!";
        }
    }
}
