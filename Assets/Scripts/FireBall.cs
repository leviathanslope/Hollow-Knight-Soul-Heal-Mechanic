using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCharacter playerCharacter = collision.GetComponent<PlayerCharacter>();
        
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(20);
        }
        Destroy(gameObject);
    }
}
