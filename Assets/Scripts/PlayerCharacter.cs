using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    AudioSource _audioSource = null;
    public Animator _animator;

    public int maxHealth = 100;
    public int currentHealth;
    public int maxSoul = 5;
    public int currentSoul;

    public HealthBar healthBar;
    public SoulBar soulBar;

    [SerializeField] AudioClip _damageSFX = null;
    [SerializeField] AudioClip _deathSFX = null;

    public UnityEvent OnDieEvent;
    public UnityEvent OnDamageEvent;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentSoul = maxSoul;
        soulBar.SetMaxSoul(maxSoul);
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (_damageSFX != null)
        {
            _audioSource.PlayOneShot(_damageSFX, 1f);
        }

        if (currentHealth <= 0)
        {
            Die();
        } else
        {
            OnDamageEvent.Invoke();
        }
    }

    public void Die()
    {
        Debug.Log("Player has been killed!");
        DisableOnDeathObjects();
    }

    private void DisableOnDeathObjects()
    {
        OnDieEvent.Invoke();
    }

    public void OnDie()
    {
        _animator.SetBool("IsDead", true);
    }

    public void OnDamage()
    {
        _animator.SetBool("IsHurt", true);
    }
}
