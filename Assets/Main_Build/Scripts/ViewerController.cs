using UnityEngine;

public sealed class ViewerController : MonoBehaviour
{
    [SerializeField] Transform headTransform;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed;
    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        RotateHead();

        MoveCharacter();
    }

    private void RotateHead()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Cursor.visible = false;
            headTransform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
            headTransform.Rotate(Vector3.right, -mouseY * rotationSpeed, Space.Self);
        }
        else
            Cursor.visible = true;
    }

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = headTransform.forward * verticalInput + headTransform.right * horizontalInput;
        movementDirection.y = 0f;

        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

        Vector3 movement = movementSpeed * Time.deltaTime * movementDirection;

        characterController.Move(movement);
    }
}

