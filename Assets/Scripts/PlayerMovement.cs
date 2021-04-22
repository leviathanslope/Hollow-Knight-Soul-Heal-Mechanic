using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D _controller;
    public Animator _animator;
    public float _runSpeed = 40f;
    float _horizontalMove = 0f;
    bool _jump = false;
    bool _crouch = false;

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            _animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        _animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
        _jump = false;
    }
}
