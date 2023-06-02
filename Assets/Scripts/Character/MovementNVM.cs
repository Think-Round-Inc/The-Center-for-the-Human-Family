using UnityEngine;
using UnityEngine.AI;

public class MovementNVM : MonoBehaviour
{
    [SerializeField] public NavMeshAgent nvm = null;

    private void Awake()
    {
        if (nvm == null)
        {
            nvm = GetComponent<NavMeshAgent>();
        }
        if (nvm == null)
        {
            nvm = gameObject.AddComponent<NavMeshAgent>();
            Debug.LogWarning(gameObject.name + " had no NavMeshAgent and now has one.");
        }
    }
    private void OnEnable()
    {
        if (nvm == null)
        {
            nvm = GetComponent<NavMeshAgent>();
        }
        if (nvm != null)
        {
            nvm.enabled = true;
        }
    }

    private void OnDisable()
    {
        if (nvm != null)
        {
            nvm.enabled = false;
        }
    }
    public void Stop()
    {
        nvm.isStopped = true;
    }
    public void MoveInDirection(Vector3 direction)
    {
        if (nvm != null && nvm.enabled)
        {
            nvm.Move(direction.normalized * nvm.speed * Time.deltaTime);
        }
    }

    public void MoveToDestination(Vector3 destination)
    {
        if (nvm != null && nvm.enabled)
        {
            nvm.SetDestination(destination);
        }
    }
    public void MoveToDestination(Transform destination)
    {
        MoveToDestination(destination.position);
    }

    public void MoveToDestination(GameObject destination)
    {
        MoveToDestination(destination.transform.position);
    }
}
