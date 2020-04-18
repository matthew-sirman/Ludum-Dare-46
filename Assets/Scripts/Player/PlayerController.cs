using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth;
    
    public float movementSpeed = 500f;
    public float accelerationRate = 0.2f;
    public float mouseSensitivity = 5f;
    public float jumpAmount = 6f;
    public float jumpControlFactor = 1f;
    public Rigidbody playerRigidbody;
    public Camera playerCamera;
    private const float MinX = 0f;
    private const float MaxX = 360f;
    private const float MinY = -90f;
    private const float MaxY = 90f;
    private Vector3 _moveDir;
    private Vector2 _mousePos;
    private bool _isJumping = false;
    private bool _pressedJump = false;
    private bool _movementLocked = false;
    private float _currentHealth;
    private MoneyManager moneyManager;

    void Start()
    {
        moneyManager = GameObject.FindWithTag("MoneyManager").GetComponent<MoneyManager>();
        _currentHealth = maxHealth;
        LockCursor();
    }

    void Update()
    {
        if (!_movementLocked)
        {
            _moveDir.x = Input.GetAxisRaw("Horizontal");
            _moveDir.y = Input.GetAxisRaw("Vertical");
            _moveDir.Normalize();
            
            _mousePos.x += Input.GetAxis("Mouse X") * mouseSensitivity + MaxX - MinX;
            _mousePos.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
            _moveDir = Vector2.zero;
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
        float a = -Mathf.Deg2Rad * t.eulerAngles.y;
        float s = Mathf.Sin(a);
        float c = Mathf.Cos(a);
        Vector2 m = new Vector2(
            _moveDir.x * c - _moveDir.y * s,
            _moveDir.x * s + _moveDir.y * c
            ) * (movementSpeed * Time.fixedDeltaTime);
        float accelerationAmount = accelerationRate;
        if (_isJumping)
        {
            accelerationAmount *= jumpControlFactor;
        }
        playerRigidbody.velocity = new Vector3(m.x, playerRigidbody.velocity.y, m.y);
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

    public void Damage(float amount)
    {
        Debug.Log(_currentHealth);
        _currentHealth -= amount;

        if (_currentHealth < 0)
        {
            Die();
        }
    }

    public void notifyEnemyKilled(EnemyType type) {
        moneyManager.enemyKilled(type);
    }

    public void Die()
    {
        // TODO: Implement this function
    }
}
