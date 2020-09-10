using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    public int manaToGain = 10;

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Manapotion is touched");
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.GetComponent<Mana>().GainMana(manaToGain);
        }
        gameObject.SetActive(false);
    }
    */
}
