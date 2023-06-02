using UnityEngine;
using Utilities;

public class PlayerController : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode rotateLeftKey = KeyCode.Q;
    [SerializeField] KeyCode rotateRightKey = KeyCode.E;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Configure")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintMultiplier = 1.5f;
    [SerializeField] float rotateSpeed = 80f;

    [Header("Reference")]
    [SerializeField] public MovementNVM movementNVM = null;
    [SerializeField] CameraControl cameraControl = null;
    [SerializeField] public Rigidbody rb = null;

    public static PlayerController Instance = null;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Duplicate Player Object will be destroyed.");
            Destroy(gameObject);
        }

        movementNVM = GetComponent<MovementNVM>();
        if (movementNVM == null)
        {
            Debug.LogWarning(gameObject.name + " has no NavMesh Controller");
        }


        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            Debug.LogWarning(gameObject.name + " had no Rigidbody and now has one.");
        }
    }
    private void tryInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit the plane
            if (hit.collider.gameObject.name == "StandMesh")
            {
                // Get the position of the intersection point
                Vector3 position = hit.point;

                // Do something with the position
                Debug.Log("press f on : " + hit.collider.gameObject.name);
                MeshRenderer meshRenderer = hit.collider.gameObject.GetComponent<MeshRenderer>();

                // Get the second material in the materials array
                Material secondMaterial = meshRenderer.materials[1];
                Debug.Log("material : " + meshRenderer.materials[1].name);
                //toggleManager.enterSinglePaintingView();
            }
        }
    }


    private void Update()
    {
        if (movementNVM != null && movementNVM.enabled)
        {
            ProcessMovement();
        }

        //if (!viewSinglePainting && Input.GetKeyDown(KeyCode.F)){
        //  //  tryInteract();
        //    viewSinglePainting = true;
        //}

        //if (viewSinglePainting && Input.GetKeyDown(KeyCode.F))
        //{
        //    toggleManager.quitSinglePaintingView();
        //    viewSinglePainting = false;
        //}

        //if (Input.GetKey(KeyCode.B))
        //{
        //    Debug.Log("toggle camera");
        //}

        //if (Input.GetKey(KeyCode.Z))
        //{
        //    transform.position += Vector3.up * 5f * Time.deltaTime;
        //}
        //if (Input.GetKey(KeyCode.C))
        //{
        //    transform.position -= Vector3.up * 5f * Time.deltaTime;
        //}

        if (cameraControl != null)
        {
            cameraControl.PanCamera();
        }
    }

    private void ProcessMovement()
    {
        // Determine Magnitude of Input to apply to movement
        Vector3 movementVector = Vector3.zero;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0f || v != 0f)
        {
            movementVector = new Vector3(h, 0f, v).normalized;
        }
        else if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            movementVector = Vector3.forward;
        }

        // Determine Actual movement speed
        float moveSpeed = movementSpeed;
        if (Input.GetKey(sprintKey))
        {
            moveSpeed *= sprintMultiplier;
        }

        // Determine Direction of Movement
        Vector3 relativeForward = transform.forward;

        if(cameraControl != null)
        {
            relativeForward = cameraControl.transform.forward;
        }

        if (movementVector != Vector3.zero)
        {
            // Get Camera forward
            if (cameraControl.GetPivot() != null)
            {
                relativeForward = cameraControl.GetPivot().GetRelativeDirectionWithMagnitude(movementVector.x, movementVector.z).normalized;
            }

            relativeForward *= moveSpeed * Time.deltaTime;
            transform.position += relativeForward;
            //movementNVM.MoveToDestination(transform.position + relativeForward);
        }
        else 
        {
            //not moving
        }

        if (Input.GetKey(rotateLeftKey))
        {
            transform.Rotate(-Vector3.up, rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(rotateRightKey))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }

    public void setPosition(Vector3 vector3)
    {
        transform.position = new Vector3(vector3.x, transform.position.y, vector3.z);

    }

    public void EnableNavMeshAgent(bool enabled)
    {
        movementNVM.nvm.enabled = enabled;
    }

}
