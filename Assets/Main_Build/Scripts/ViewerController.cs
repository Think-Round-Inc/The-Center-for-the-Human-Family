using UnityEngine;

public sealed class ViewerController : MonoBehaviour
{
    public bool viewerControllerActive = true;
    [SerializeField] Transform headTransform;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed;
    [SerializeField, Range(0, 90)] int maxHeadRotateAngle = 80;
    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (!viewerControllerActive) return;
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

            // Calculate the rotation around the X-axis
            Vector3 currentRotation = headTransform.localRotation.eulerAngles;
            float newAngleX = currentRotation.x - mouseY * rotationSpeed;
            newAngleX = WrapAngle(newAngleX); // Wrap the angle to -180 to 180 degrees
            newAngleX = Mathf.Clamp(newAngleX, -maxHeadRotateAngle, maxHeadRotateAngle);

            // Apply the new rotation around the X-axis
            headTransform.localRotation = Quaternion.Euler(newAngleX, currentRotation.y, 0f);

            // Rotate around the Y-axis separately
            headTransform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
        }
        else
        {
            Cursor.visible = true;
        }
    }

    // Wrap the angle to -180 to 180 degrees
    private float WrapAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f)
            return angle - 360f;
        if (angle < -180f)
            return angle + 360f;
        return angle;
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


