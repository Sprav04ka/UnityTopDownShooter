using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject standardEnemyPrefab; 
    public GameObject fastEnemyPrefab;     
    public GameObject armoredEnemyPrefab;  

    public float initialSpawnInterval = 2f; 
    public float minSpawnInterval = 0.5f;   
    public float spawnIntervalDecrease = 0.1f; 
    public float intervalDecreaseTime = 10f; 

    public LayerMask groundLayer; 

    public float mapMinX = -50f; 
    public float mapMaxX = 50f;  
    public float mapMinZ = -50f; 
    public float mapMaxZ = 50f;  

    private Camera mainCamera;
    private float currentSpawnInterval;
    private float timeSinceLastSpawn;
    private float timeSinceLastDecrease;

    private void Start()
    {
        mainCamera = Camera.main;
        currentSpawnInterval = initialSpawnInterval;
        timeSinceLastSpawn = 0f;
        timeSinceLastDecrease = 0f;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastDecrease += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }

        if (timeSinceLastDecrease >= intervalDecreaseTime && currentSpawnInterval > minSpawnInterval)
        {
            currentSpawnInterval = Mathf.Max(currentSpawnInterval - spawnIntervalDecrease, minSpawnInterval);
            timeSinceLastDecrease = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition;
        bool validSpawnPosition = false;

        // Generate spawn points until we find a valid one
        do
        {
            // Generate a random position on the map
            float randomX = Random.Range(mapMinX, mapMaxX);
            float randomZ = Random.Range(mapMinZ, mapMaxZ);
            spawnPosition = new Vector3(randomX, 0, randomZ);

            // Check that the spawn point is outside the visible camera area
            if (IsOutsideCameraView(spawnPosition))
            {
                // Check for the presence of land under the spawn point
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(spawnPosition.x, 1000, spawnPosition.z), Vector3.down, out hit, Mathf.Infinity, groundLayer))
                {
                    if (hit.collider != null)
                    {
                        spawnPosition.y = hit.point.y + 0.5f; // Set the Y coordinate to 0.5 above ground level
                        validSpawnPosition = true;
                    }
                }
            }
        } while (!validSpawnPosition);

        // Spawn the enemy
        Instantiate(GetRandomEnemyPrefab(), spawnPosition, Quaternion.identity);
    }

    private bool IsOutsideCameraView(Vector3 position)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(position);
        return viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        float randomValue = Random.value * 100f;

        if (randomValue < 60f) // 60% chance for a standard enemy
        {
            return standardEnemyPrefab;
        }
        else if (randomValue < 90f) // 30% chance for fast enemy
        {
            return fastEnemyPrefab;
        }
        else // 10% chance for armored enemy
        {
            return armoredEnemyPrefab;
        }
    }
}