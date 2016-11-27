using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Takes care of the damage of the player.
public class PlayerDamage : MonoBehaviour {

    [Header("Player Damage")]
    [ReadOnly]
    public int stocks = 3;
    public float playerDamage;
    public GameObject associatedCounter;

    [Header("Player Status")]
    public bool isRespawning;
    public bool isDead;

    // Internal
    private Text _playerName;
    private Text _damageText;
    private Text _countdownText;
    private WaitForSeconds _respawnTime;
    private PlayerMovement _playerMovement;
    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _respawnTime = new WaitForSeconds(1f);
        _playerMovement = GetComponent<PlayerMovement>();
        _audioSource = GetComponent<AudioSource>();
        _playerName = associatedCounter.transform.GetChild(0).GetComponent<Text>();
        _damageText = associatedCounter.transform.GetChild(1).GetComponent<Text>();
        _countdownText = associatedCounter.transform.GetChild(2).GetComponent<Text>();
    }

    // Applies damage to the player.
    public void ApplyDamage(int damageValue, float knockbackForce)
    {
        // Checks if damage hasn't overflown.
        if(playerDamage < 420)
        {
            _audioSource.PlayOneShot(GameManager.gameManager.damageClip);
            GameManager.gameManager.CreateGore(transform.position, 4);
            playerDamage += damageValue;
            UpdateCanvas();
        }

        // Gives knockback.
        _playerMovement.ReceiveKnockback(knockbackForce);
    }

    // Updates the Canvas with the players current damage.
    public void UpdateCanvas()
    {
        _damageText.text = playerDamage + "% [" + stocks + "]";
    }

    // Kills the player.
    public void KillPlayer()
    {
        // Displays the text to gray.
        _countdownText.enabled = true;
        _playerName.color = Color.gray;
        _damageText.color = Color.gray;
        GameManager.gameManager.CreateLava(transform.position);
        _audioSource.PlayOneShot(GameManager.gameManager.deathClip);

        // Nulls the damage and checks if the player still has stocks remaining.
        isRespawning = true;
        if(stocks > 0)
        {
            playerDamage = 0;
            stocks--;
            StartCoroutine(RespawnTimer());
        }
        else
        {
            GameManager.gameManager.KillPlayer(gameObject);
            _countdownText.text = "RIP!";
        }

        // Updates the canvas.
        UpdateCanvas();
    }

    // Waits for the player respawn.
    IEnumerator RespawnTimer()
    {
        // Shows a waiting timer.
        for (int i = GameManager.gameManager.respawnTime; i != 0 ; i--)
        {
            _countdownText.text = i.ToString();
            yield return _respawnTime;
        }

        // Respawns the player.
        _playerName.color = Color.white;
        _damageText.color = Color.white;
        _countdownText.enabled = false;
        isRespawning = false;
        GetComponent<PlayerMovement>().RespawnPlayerPos();
    }
}
