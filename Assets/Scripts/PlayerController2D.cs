using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float _crouchSpeed = 0.36f;
    [Range(0, 0.3f)] [SerializeField] private float _movementSmoothing = 0.05f;
    [SerializeField] private bool _airControl = false;
    [SerializeField] private LayerMask _thisIsGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _ceilingCheck;
    [SerializeField] private Collider2D _crouchDisableCollider;

    AudioSource audioSource = null;

    const float _groundedRadius = 0.05f;
    private bool _grounded;
    const float _ceilingRadius = 0.2f;
    private Rigidbody2D _rigidbody2D;
    private bool _facingRight = true;
    private Vector3 _velocity = Vector3.zero;
    bool isDead = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [Header("Feedback")]
    [SerializeField] AudioClip deathSFX = null;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool _wasCrouching = false;

    private void Update()
    {
        if (isDead)
        {
            return;
        }
    }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        if (OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }
    }
    private void FixedUpdate()
    {
        bool _wasGrounded = _grounded;
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _thisIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!_wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }
    public void Move(float move, bool crouch, bool jump)
    {
        if (crouch)
        {
            if (Physics2D.OverlapCircle(_ceilingCheck.position, _ceilingRadius, _thisIsGround))
            {
                crouch = true;
            }
        }

        if (_grounded || _airControl)
        {
            if (crouch)
            {
                if (!_wasCrouching)
                {
                    _wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= _crouchSpeed;

                if (_crouchDisableCollider != null)
                {
                    _crouchDisableCollider.enabled = false;
                }
            }
            else
            {
                if (_crouchDisableCollider != null)
                {
                    _crouchDisableCollider.enabled = true;
                }

                if (_wasCrouching)
                {
                    _wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2D.velocity.y);
            _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            if (move > 0 && !_facingRight)
            {
                Flip();
            }

            else if (move < 0 && _facingRight)
            {
                Flip();
            }
        }

        if (_grounded && jump)
        {
            _grounded = false;
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
        }
    }
    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
