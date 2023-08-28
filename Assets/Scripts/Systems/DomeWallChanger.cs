using System.Collections;
using UnityEngine;

public sealed class DomeWallChanger : MonoBehaviour
{
    [SerializeField] bool isActive;
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
}
