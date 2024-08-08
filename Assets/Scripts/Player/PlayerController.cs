using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    public bool IsEnabled
    {
        get
        {
            return _isEnabled;
        }
        set
        {
            _isEnabled = value;
            if (_isEnabled)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    [SerializeField] bool _isEnabled;
    public bool CanToggleEnable = true;
    public float Speed = 5.0f;
    public float MouseSensitivity = 100.0f;
    public Transform Head;
    public float Friction = 0.9f;

    private Rigidbody rb;
    CapsuleCollider _capsuleCollider;
    private float rotationY = 0.0f;

    private void OnValidate()
    {
        IsEnabled = _isEnabled;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        IsEnabled = _isEnabled;
    }

    private void Update()
    {
        if (CanToggleEnable && Input.GetMouseButtonDown(1))
        {
            IsEnabled = !IsEnabled;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        if (!IsEnabled) return;
        RotateHead();
    }

    void MovePlayer()
    {
        // Handle movement
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector3 moveInput = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveInput.Normalize();
        if (!IsEnabled)
        {
            moveInput = Vector3.zero;
        }

        Vector3 horizonVel = rb.velocity;
        horizonVel.y = 0f;

        float moveSpeed = (Input.GetKey(KeyCode.LeftShift) ? 1.5f : 1f) * Speed;

        rb.velocity = Friction * moveSpeed * moveInput + Vector3.up * rb.velocity.y + horizonVel * (1f - Friction);
    }

    void RotateHead()
    {
        // Using raw mouse input
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        Head.localEulerAngles = Vector3.right * rotationY;
        rb.MoveRotation(Quaternion.Euler(0f, rb.rotation.eulerAngles.y + mouseX, 0f));
    }
}
