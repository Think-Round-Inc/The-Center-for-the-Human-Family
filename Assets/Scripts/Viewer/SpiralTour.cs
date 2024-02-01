using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public sealed class SpiralTour : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool viewerControllerActive = true;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 3f;
    [SerializeField] private float pauseDuration = 2f;

    [Header("Target Tags")]
    [SerializeField] private string targetTagViewingPoint = "ViewingPoint";
    [SerializeField] private string targetTagPausePoint = "PausePoint";
    [SerializeField] private string targetTagScreen = "Display";

    [Header("Target Lists")]
    [SerializeField] private List<GameObject> targetPositions = new List<GameObject>();
    [SerializeField] private List<GameObject> targetRotations = new List<GameObject>();

    private int currentTargetIndex = 0;
    private float currentTime = 0f;

    private void Start()
    {
        CollectObjectsWithTag(targetTagViewingPoint, targetPositions);
        CollectObjectsWithTag(targetTagPausePoint, targetPositions);
        CollectObjectsWithTag(targetTagScreen, targetRotations);

        SortLists(targetPositions, targetRotations);
    }

    private void CollectObjectsWithTag(string targetTag, List<GameObject> targetList)
    {
        targetList.AddRange(GameObject.FindGameObjectsWithTag(targetTag));
    }

    private void SortLists(List<GameObject> targetPositionsList, List<GameObject> targetRotationsList)
    {
        targetPositionsList.Sort((obj1, obj2) => CompareNames(obj1.name, obj2.name));
        targetRotationsList.Sort((obj1, obj2) => CompareNames(obj1.name, obj2.name));
    }

    private int CompareNames(string name1, string name2)
    {
        // Define the regular expression pattern
        string pattern = @"([a-zA-Z]{2})([a-zA-Z]{2})(\d{2})([a-zA-Z]+)(-)(\d+)";

        // Match the patterns for both names
        Match match1 = Regex.Match(name1, pattern);
        Match match2 = Regex.Match(name2, pattern);

        // Check if both matches were successful
        if (match1.Success && match2.Success)
        {
            int part3_1 = int.Parse(match1.Groups[3].Value);
            int part3_2 = int.Parse(match2.Groups[3].Value);

            // Sort first by Part 3 in descending order
            int result = part3_2.CompareTo(part3_1);

            if (result == 0) // If Part 3 values are equal, sort by Part 4 in ascending order
            {
                string part4_1 = match1.Groups[4].Value;
                string part4_2 = match2.Groups[4].Value;

                result = part4_1.CompareTo(part4_2);

                if (result == 0) // If Part 4 values are equal, sort by Part 5 in ascending order
                {
                    int part5_1 = int.Parse(match1.Groups[6].Value);
                    int part5_2 = int.Parse(match2.Groups[6].Value);

                    result = part5_1.CompareTo(part5_2);
                }
            }

            return result;
        }

        // If the pattern doesn't match, consider them equal
        return string.Compare(name1, name2);
    }

    private void FixedUpdate()
    {
        if (viewerControllerActive) return;
        MoveAndRotateTowardsTargets();
    }

    public void SetViewerControlsActive(bool value) => viewerControllerActive = value;

    private void MoveAndRotateTowardsTargets()
    {
        if (targetPositions.Count == 0 || currentTargetIndex == targetPositions.Count)
            return;

        Transform currentTargetPosition = targetPositions[currentTargetIndex].transform;
        Transform currentTargetRotation = targetRotations[currentTargetIndex].transform;

        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition.position, moveSpeed * Time.fixedDeltaTime);

        Vector3 directionToFace = currentTargetRotation.position - transform.position;

        if (directionToFace != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(directionToFace);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.fixedDeltaTime);
        }

        float distanceToTarget = Vector3.Distance(transform.position - new Vector3(0f, 0.985f, 0f), currentTargetPosition.position);

        if (distanceToTarget <= 0.1f && Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToFace)) < 0.1f)
        {
            if (targetPositions[currentTargetIndex].CompareTag(targetTagPausePoint))
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
