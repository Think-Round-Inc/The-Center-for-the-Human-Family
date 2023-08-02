using UnityEngine;

public sealed class ViewerController : MonoBehaviour
{
    public bool viewerControllerActive = true;
    [SerializeField] Transform headTransform;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed;
    [SerializeField, Range(0, 90)] int maxHeadRotateAngle = 80;

    void FixedUpdate()
    {
        if (!viewerControllerActive) return;
        RotateViewer();
        MoveCharacter();
    }

    private void RotateViewer()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Cursor.visible = false;

            // Calculate the rotation around the X-axis
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            float newAngleX = currentRotation.x - mouseY * rotationSpeed;
            newAngleX = WrapAngle(newAngleX); // Wrap the angle to -180 to 180 degrees
            newAngleX = Mathf.Clamp(newAngleX, -maxHeadRotateAngle, maxHeadRotateAngle);

            // Apply the new rotation around the X-axis
            transform.localRotation = Quaternion.Euler(newAngleX, currentRotation.y, 0f);

            // Rotate around the Y-axis separately
            transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
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

        // Use the main camera's forward direction instead of headTransform.forward
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 movementDirection = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;
        movementDirection.y = 0f;

        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

        Vector3 movement = movementSpeed * Time.deltaTime * movementDirection;
        movement.y = 0f;

        transform.Translate(movement, Space.World);
    }
}



