using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;

    Vector2Int currentGridPos = Vector2Int.zero;
    GameObject currentPulpit;

    float minDestroyTime;
    float maxDestroyTime;
    float spawnTime;

    const float CELL_SIZE = 9f;

    void Start()
    {
        // Load from JSON
        minDestroyTime = GameConfigLoader.Config.pulpit_data.min_pulpit_destroy_time;
        maxDestroyTime = GameConfigLoader.Config.pulpit_data.max_pulpit_destroy_time;
        spawnTime = GameConfigLoader.Config.pulpit_data.pulpit_spawn_time;

        SpawnFirstPulpit();
    }

    void SpawnFirstPulpit()
    {
        currentGridPos = Vector2Int.zero;
        currentPulpit = Instantiate(pulpitPrefab, GridToWorld(currentGridPos), Quaternion.identity);

        StartCoroutine(PulpitLifecycle(currentPulpit, currentGridPos));
    }

    IEnumerator PulpitLifecycle(GameObject oldPulpit, Vector2Int oldPos)
    {
        // Wait before spawning next pulpit
        yield return new WaitForSeconds(spawnTime);

        // Pick random neighbor
        Vector2Int nextPos = GetRandomNeighbour(oldPos);

        // Spawn the next one
        GameObject newPulpit = Instantiate(pulpitPrefab, GridToWorld(nextPos), Quaternion.identity);

        // Wait remaining lifetime before destroying old
        float remainingLife = Random.Range(minDestroyTime, maxDestroyTime);
        yield return new WaitForSeconds(remainingLife);

        Destroy(oldPulpit);

        // Move on
        currentGridPos = nextPos;
        currentPulpit = newPulpit;

        StartCoroutine(PulpitLifecycle(newPulpit, nextPos));
    }

    Vector2Int GetRandomNeighbour(Vector2Int pos)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>()
        {
            pos + Vector2Int.up,
            pos + Vector2Int.down,
            pos + Vector2Int.left,
            pos + Vector2Int.right
        };

        return neighbours[Random.Range(0, neighbours.Count)];
    }

    Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * CELL_SIZE, 0f, gridPos.y * CELL_SIZE);
    }
}
