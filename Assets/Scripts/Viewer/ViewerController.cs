using System.Collections;
using System.Collections.Generic;
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
        if (!viewerControllerActive)
        {
            SpiralTour();
        }
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

            Vector3 currentRotation = transform.localRotation.eulerAngles;
            float newAngleX = currentRotation.x - mouseY * rotationSpeed;
            newAngleX = WrapAngle(newAngleX);
            newAngleX = Mathf.Clamp(newAngleX, -maxHeadRotateAngle, maxHeadRotateAngle);

            transform.localRotation = Quaternion.Euler(newAngleX, currentRotation.y, 0f);

            transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
        }
        else
        {
            Cursor.visible = true;
        }
    }

    private float WrapAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f)
            return angle - 360f;
        if (angle < -180f)
            return angle + 360f;
        return angle;
    }

    public void SetViewerControlsActive(bool value) => viewerControllerActive = value;

    private void MoveCharacter()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 movementDirection = cameraForward * verticalInput + Camera.main.transform.right * horizontalInput;
        movementDirection.y = 0f;

        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

        Vector3 movement = movementSpeed * Time.deltaTime * movementDirection;
        movement.y = 0f;

        transform.Translate(movement, Space.World);
    }

    public List<Transform> targetPositions;
    public List<Transform> targetRotations;
    public float moveSpeed = 3f;
    public float rotSpeed = 3f; 
    public float pauseDuration = 2f; // Adjust the duration of the pause
    
    private float currentTime = 0f;
    private int currentTargetIndex = 0;

    private void SpiralTour()
    {
        // Check if there are target positions and target rotations
        if (targetPositions.Count == 0 || currentTargetIndex==targetPositions.Count)
            return;

        // Move towards the current target position
        transform.position = Vector3.MoveTowards(transform.position, targetPositions[currentTargetIndex].position, moveSpeed * Time.fixedDeltaTime);

        Vector3 directionToFace = targetRotations[currentTargetIndex].position - transform.position;

        if (directionToFace != Vector3.zero)
        {
            // Create a rotation based on the direction
            Quaternion rotation = Quaternion.LookRotation(directionToFace);

            // Smoothly rotate towards the desired direction
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);
        }

        // Check if the object is close enough to the current target position
        if (Vector3.Distance(transform.position - new Vector3(0f, 0.985f, 0f), targetPositions[currentTargetIndex].position) <= 0.1f && Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToFace)) < 0.1f)
        {
            if(targetPositions[currentTargetIndex].CompareTag("PausePoint"))
            {
                currentTargetIndex++;
                currentTime = 0f;
            }
            else
            {
                currentTime += 0.01f;
                if (currentTime > pauseDuration)
                {
                    currentTargetIndex++;
                    currentTime = 0f;
                }
            }
        }
    }
}