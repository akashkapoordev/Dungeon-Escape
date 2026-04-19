using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 1f;


    private bool _canDash = true;
    private bool _isDashing = false;
    private Rigidbody2D _rb;
    private PlayerMovement _movement;
    private PlayerInputActions _inputActions;

    public bool IsDashing => _isDashing;
    public float CooldownRemaining { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
        _inputActions = new PlayerInputActions();
    }

private void OnEnable()
    {
        if (_inputActions == null) _inputActions = new PlayerInputActions();
        _inputActions.Gameplay.Enable();
        _inputActions.Gameplay.Dash.performed += OnDash;
    }


    private void OnDisable()
    {
        _inputActions.Gameplay.Dash.performed -= OnDash;
        _inputActions.Gameplay.Disable();
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        if(_canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

private IEnumerator DashCoroutine()
    {
        _canDash = false;
        _isDashing = true;

 
        Vector2 dashDir = _inputActions.Gameplay.Move.ReadValue<Vector2>();
        if (dashDir == Vector2.zero) dashDir = Vector2.right; 

        _rb.linearVelocity = dashDir.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);
        _rb.linearVelocity = Vector2.zero;
        _isDashing = false;

        CooldownRemaining = dashCooldown;
        while (CooldownRemaining > 0)
        {
            CooldownRemaining -= Time.deltaTime;
            yield return null;
        }

        _canDash = true;
    }
}
