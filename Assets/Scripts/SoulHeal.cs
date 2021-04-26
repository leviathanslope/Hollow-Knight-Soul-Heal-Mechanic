using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoulHeal : MonoBehaviour
{

    AudioSource _audioSource = null;
    public Animator animator;
    public PlayerCharacter playerCharacter;

    [SerializeField] AudioClip _healingSFX = null;

    public UnityEvent OnHealEvent;

    private float _startTime = 0f;
    private float _timer = 0f;
    public float holdTime = 3.0f;

    private bool _held = false;

    private void Start()
    {
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _startTime = Time.time;
            _timer = _startTime;
        }

        if (Input.GetKey(KeyCode.Q) && _held == false) {
            _timer += Time.deltaTime;

            if (_timer > (_startTime + holdTime) && playerCharacter.currentSoul != 0)
            {
                _held = true;
                playerCharacter.Heal(20);
                playerCharacter.SoulUse(1);
                OnHealEvent.Invoke();
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            _held = false;
        }
    }

    public void OnHeal()
    {
        animator.SetBool("IsHealing", true);
    }
}
