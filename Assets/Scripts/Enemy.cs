using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        PlayerCharacter playerCharacter = collision.GetComponent<PlayerCharacter>();
        if (collision.gameObject)
        {
            playerCharacter.TakeDamage(20);
        }
    }
}
