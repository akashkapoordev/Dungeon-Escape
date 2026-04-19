using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private PlayerInputActions _inputActions;
    private PlayerDash _dash;



private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _dash = GetComponent<PlayerDash>();
        _inputActions = new PlayerInputActions();
    }

private void OnEnable()
    {
        if (_inputActions == null) _inputActions = new PlayerInputActions();
        _inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Gameplay.Disable();
    }

    private void Update()
    {
        _moveInput = _inputActions.Gameplay.Move.ReadValue<Vector2>();
    }

private void FixedUpdate()
    {
        if (_dash != null && _dash.IsDashing) return;

        _rb.MovePosition(_rb.position + _moveInput * moveSpeed * Time.deltaTime);
    }
}
