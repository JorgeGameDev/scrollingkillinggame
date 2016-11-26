using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Takes care of the damage of the player.
public class PlayerDamage : MonoBehaviour {

    [Header("Player Damage")]
    public int stocks = 3;
    public float playerDamage;
    public Text associatedCounter;

    // Applies damage to the player.
    public void ApplyDamage(int damageValue)
    {
        playerDamage += damageValue;
        associatedCounter.text = playerDamage + "% [" + stocks + "]";
    }
}
