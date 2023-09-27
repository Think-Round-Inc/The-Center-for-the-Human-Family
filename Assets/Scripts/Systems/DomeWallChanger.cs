using System.Collections;
using UnityEngine;

public enum DomeWallState
{
    Art,
    Religion
}

public sealed class DomeWallChanger : MonoBehaviour
{
    public static DomeWallState CurrentDomeWallState = DomeWallState.Religion;
    [SerializeField] DomeWallState currentDomeWallState = DomeWallState.Religion;
    [SerializeField] bool isActive = true;
    [SerializeField] float timeToChangeWalls;
    [SerializeField] float timeBetweenWallChanges;
    [SerializeField] DomeWall[] faithWalls;
    [SerializeField] DomeWall[] faunaWalls;
    [SerializeField] DomeWall[] infoWalls;
    private float currentTime;

    private void Start()
    {
        currentTime = Mathf.Max(1, timeToChangeWalls);
    }

    public void SetDomeWallChangerActive(bool isActive) => this.isActive = isActive;

    private void Update()
    {
        if (!isActive) return;
        currentTime = currentTime < 0 ? 0 : currentTime -= Time.deltaTime;
        if (currentTime == 0)
        {
            currentTime = Mathf.Max(1, timeToChangeWalls);
            StartCoroutine(ChangeWalls(faithWalls));
            StartCoroutine(ChangeWalls(faunaWalls));
            StartCoroutine(ChangeWalls(infoWalls));
        }
    }

    public IEnumerator ChangeWalls(DomeWall[] walls)
    {
        for (int i = 0; i < walls.Length; i++)
        {
            yield return new WaitForSeconds(Mathf.Max(0, timeBetweenWallChanges));
            walls[i].SwitchWallSprite();
        }
    }

    private void OnValidate() => CurrentDomeWallState = currentDomeWallState;
}
