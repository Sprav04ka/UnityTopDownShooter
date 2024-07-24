using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour

{
    public GameObject[] weaponBonusPrefabs; 
    public GameObject[] powerUpPrefabs; 
    public Transform playerTransform;
    public Camera mainCamera;
    public float weaponSpawnInterval = 10f;
    public float powerUpSpawnInterval = 27f;
    public LayerMask groundLayer;

    public float mapMinX = -50f;
    public float mapMaxX = 50f;
    public float mapMinZ = -50f;
    public float mapMaxZ = 50f;

    void Start()
    {
        StartCoroutine(SpawnWeaponBonus());
        StartCoroutine(SpawnPowerUpBonus());
    }

    IEnumerator SpawnWeaponBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(weaponSpawnInterval);

            Vector3 spawnPosition = GetRandomSpawnPosition();

            if (spawnPosition != Vector3.zero)
            {
                // Select a random weapon that does not match the player's current weapon
                int randomIndex = Random.Range(0, weaponBonusPrefabs.Length);
                while (randomIndex + 1 == playerTransform.GetComponent<Shooting>().selectedWeapon)
                {
                    randomIndex = Random.Range(0, weaponBonusPrefabs.Length);
                }

                Debug.Log($"Spawning weapon bonus at {spawnPosition} with index {randomIndex}");
                GameObject bonus = Instantiate(weaponBonusPrefabs[randomIndex], spawnPosition, Quaternion.identity);

                // Destroy the bonus if it is not picked up within 5 seconds
                Destroy(bonus, 5f);
            }
            else
            {
                Debug.Log("Failed to find a valid spawn position for weapon bonus");
            }
        }
    }

    IEnumerator SpawnPowerUpBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnInterval);

            Vector3 spawnPosition = GetRandomSpawnPosition();

            if (spawnPosition != Vector3.zero)
            {
                // Select a random powerup
                int randomIndex = Random.Range(0, powerUpPrefabs.Length);

                Debug.Log($"Spawning power-up at {spawnPosition} with index {randomIndex}");
                GameObject bonus = Instantiate(powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);

                // Destroy the bonus if it is not picked up within 5 seconds
                Destroy(bonus, 5f);
            }
            else
            {
                Debug.Log("Failed to find a valid spawn position for power-up");
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        int maxAttempts = 100; // Limit on number of attempts
        for (int i = 0; i < maxAttempts; i++)
        {
            // Calculate a random position within a manually specified area
            float randomX = Random.Range(mapMinX, mapMaxX);
            float randomZ = Random.Range(mapMinZ, mapMaxZ);

            Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ);

            // Check that the position is within the camera's view
            if (IsPositionInCameraView(spawnPosition))
            {
                // Check that the position is not occupied
                Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 0.5f, groundLayer);
                if (hitColliders.Length == 0)
                {
                    Debug.Log($"Found valid position at {spawnPosition}");
                    return spawnPosition;
                }
                else
                {
                    Debug.Log($"Position at {spawnPosition} is occupied");
                }
            }
        }

        // Return Vector3.zero if a suitable position could not be found
        return Vector3.zero;
    }


    bool IsPositionInCameraView(Vector3 position)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}