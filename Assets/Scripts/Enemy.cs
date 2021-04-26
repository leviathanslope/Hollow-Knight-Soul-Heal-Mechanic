using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        PlayerCharacter playerCharacter = _colInfo.collider.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(20);
        }
    }
}
