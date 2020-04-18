using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 15f;
    public float accelerationRate = 1f;
    public float mouseSensitivity = 30f;
    public float jumpAmount = 10f;
    public float jumpControlFactor = 0.1f;
    public Rigidbody playerRigidbody;
    public Camera playerCamera;

    private const float MinX = 0f;
    private const float MaxX = 360f;
    private const float MinY = -90f;
    private const float MaxY = 90f;

    private const float MoveThreshold = 0.05f;

    private Vector2 _targetMoveDirection;
    private Vector3 _movementDir;
    private Vector2 _mousePos;

    private bool _isJumping = false;
    private bool _pressedJump = false;

    private bool _movementLocked = false;

    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        if (!_movementLocked)
        {
            _targetMoveDirection.x = Input.GetAxisRaw("Horizontal");
            _targetMoveDirection.y = Input.GetAxisRaw("Vertical");
            if (_targetMoveDirection.sqrMagnitude > 1)
            {
                _targetMoveDirection.Normalize();
            }
            
            _mousePos.x += Input.GetAxis("Mouse X") * mouseSensitivity + MaxX - MinX;
            _mousePos.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
            _targetMoveDirection = Vector2.zero;
        }

        _mousePos.x %= MaxX - MinX;
        _mousePos.x += MinX;

        _mousePos.y = Mathf.Clamp(_mousePos.y, MinY, MaxY);

        _pressedJump = Input.GetButton("Jump");

        _isJumping = !Mathf.Approximately(playerRigidbody.velocity.y, 0.0f);

        if (Input.GetButtonDown("Cancel"))
        {
            _movementLocked = !_movementLocked;
        }
    }

    private void FixedUpdate()
    {
        var t = transform;

        Vector3 targetDir = t.forward * _targetMoveDirection.y + t.right * _targetMoveDirection.x;
        Vector3 delta = targetDir - _movementDir;
        float targetDist = delta.magnitude;
        if (!Mathf.Approximately(targetDist, 0f))
        {
            float accelerationAmount = accelerationRate;
            if (_isJumping)
            {
                accelerationAmount *= jumpControlFactor;
            }
        
            delta /= targetDist;
            _movementDir += delta * Mathf.Min(accelerationAmount, targetDist);

        }

        if (_movementDir.sqrMagnitude >= MoveThreshold)
        {
            t.position += _movementDir * (movementSpeed * Time.fixedDeltaTime);
        }

        t.rotation = Quaternion.Euler(0, _mousePos.x, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(_mousePos.y, 0f, 0f);

        if (!_isJumping && _pressedJump)
        {
            playerRigidbody.velocity += Vector3.up * jumpAmount; 
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMovement()
    {
        _movementLocked = true;
    }

    public void UnlockMovement()
    {
        _movementLocked = false;
    }
}
