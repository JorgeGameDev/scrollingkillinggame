using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Manages the global variables of the UI.
public class GameManager : MonoBehaviour {

    // Manages all the players.
    public static GameManager gameManager;

    [Header("Game Flow")]
    public bool isGameOver;
    public int numberPlayers = 4;
    public int respawnTime;
    public GameObject fleshBase;
    public Sprite[] fleshSprites;
    public Sprite[] lavaSprites;

    [Header("Player Manager")]
    public List<GameObject> availableClasses;

    [Header("Audio")]
    public AudioClip damageClip;
    public AudioClip deathClip;

    [Header("UI Elements")]
    public GameObject playerText;
    public RectTransform playerRect;
    public GameObject gameOverCanvas;
    public Text winnerText;

    // Internal.
    private List<GameObject> _playersAlive = new List<GameObject>();
    private int _currentController;

    // Use this for early initialization.
    void Awake()
    {
        gameManager = this;
    }

    // Use this for initialization
    void Start () {
        // Selects a random class per player.
        for(int i = 0; i < numberPlayers; i++)
        {
            int randomClass = Random.Range(0, availableClasses.Count);
            GameObject player = availableClasses[randomClass];
            availableClasses.Remove(player);

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
            _playersAlive.Add(newPlayer);
        }
    }

    // Update is called every frame.
    void Update()
    {
        // Only allows input if it's game over.
        if(isGameOver)
        {
            // Reloads the game scene.
            if (Input.GetButtonDown("Joystick 1 Jump") || Input.GetButtonDown("Joystick 2 Jump") || Input.GetButtonDown("Joystick 3 Jump") || Input.GetButtonDown("Joystick 4 Jump"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
      
            // Loads the menu scene.
            if (Input.GetButtonDown("Joystick 1 Attack") || Input.GetButtonDown("Joystick 2 Attack") || Input.GetButtonDown("Joystick 3 Attack") || Input.GetButtonDown("Joystick 4 Attack"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
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
    public void KillPlayer(GameObject player)
    {
        // Removes this player from the list.
        _playersAlive.Remove(player);

        // Checks which player is still alive.
        if (_playersAlive.Count == 1)
        {
            // Initiailzes the string of the winning player.
            string playerAlive = _playersAlive[0].GetComponent<ClassInfo>().className;

            // Updates the canvas.
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
            winnerText.text = playerAlive + " Wins!";
            isGameOver = true;
        }
    }
}
