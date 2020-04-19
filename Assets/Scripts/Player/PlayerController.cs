using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHealth;
    
    public float movementSpeed = 15f;
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

    private const float LowerMovementSnapThreshold = 0.1f;
    
    private Vector3 _targetMoveDir;
    private Vector3 _velocity;
    
    private Vector2 _mousePos;
    
    private bool _isJumping = false;
    private bool _pressedJump = false;
    private bool _movementLocked = false;
    private float _currentHealth;
    
    private MoneyManager moneyManager;

    public bool locked
    {
        get => _movementLocked;
        private set => _movementLocked = value;
    }

    void Start()
    {
        moneyManager = GameObject.FindWithTag("MoneyManager").GetComponent<MoneyManager>();
        _currentHealth = maxHealth;
        LockCursor();
    }

    void Update()
    {
        if (!locked)
        {
            _targetMoveDir.x = Input.GetAxisRaw("Horizontal");
            _targetMoveDir.y = 0f;
            _targetMoveDir.z = Input.GetAxisRaw("Vertical");
            _targetMoveDir.Normalize();
            
            _mousePos.x += Input.GetAxis("Mouse X") * mouseSensitivity + MaxX - MinX;
            _mousePos.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        }
        else
        {
            _targetMoveDir = Vector2.zero;
        }

        _mousePos.x %= MaxX - MinX;
        _mousePos.x += MinX;

        _mousePos.y = Mathf.Clamp(_mousePos.y, MinY, MaxY);

        _pressedJump = Input.GetButton("Jump");

        _isJumping = !Mathf.Approximately(playerRigidbody.velocity.y, 0.0f);

        if (Input.GetButtonDown("Cancel"))
        {
            locked = !locked;

            if (locked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    private void FixedUpdate()
    {
        var t = transform;
        // float a = -Mathf.Deg2Rad * t.eulerAngles.y;
        // float s = Mathf.Sin(a);
        // float c = Mathf.Cos(a);
        // Vector2 m = new Vector2(
        //     _moveDir.x * c - _moveDir.y * s,
        //     _moveDir.x * s + _moveDir.y * c
        //     ) * (movementSpeed * Time.fixedDeltaTime);
        // float accelerationAmount = accelerationRate;
        // if (_isJumping)
        // {
        //     accelerationAmount *= jumpControlFactor;
        // }
        // playerRigidbody.velocity = new Vector3(m.x, playerRigidbody.velocity.y, m.y);

        Vector3 delta = _targetMoveDir - _velocity;
        float dist = delta.magnitude;

        if (dist > LowerMovementSnapThreshold)
        {
            delta /= dist;

            float accelerationAmount = accelerationRate;
            if (_isJumping)
            {
                accelerationAmount *= jumpControlFactor;
            }
            _velocity += delta * Mathf.Min(accelerationAmount, dist);
        }
        else
        {
            _velocity = _targetMoveDir;
        }

        float vx = _velocity.x * Time.deltaTime * movementSpeed;
        float vz = _velocity.z * Time.deltaTime * movementSpeed;

        t.position += t.forward * vz + t.right * vx;

        if (!_isJumping && _pressedJump)
        {
            playerRigidbody.velocity += Vector3.up * jumpAmount; 
        }
        
        t.rotation = Quaternion.Euler(0, _mousePos.x, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(_mousePos.y, 0f, 0f);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMovement()
    {
        locked = true;
    }

    public void UnlockMovement()
    {
        locked = false;
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
