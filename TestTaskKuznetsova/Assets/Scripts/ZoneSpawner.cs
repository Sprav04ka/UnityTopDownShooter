using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour

{
    public GameObject slowZonePrefab;
    public GameObject deathZonePrefab;
    public float mapMinX = -50f;
    public float mapMaxX = 50f;
    public float mapMinZ = -50f;
    public float mapMaxZ = 50f;

    public int slowZoneCount = 3;
    public float slowZoneRadius = 3f;

    public int deathZoneCount = 2;
    public float deathZoneRadius = 1f;

    private List<Vector3> zonePositions = new List<Vector3>();

    void Start()
    {
        GenerateZones(slowZonePrefab, slowZoneCount, slowZoneRadius);
        GenerateZones(deathZonePrefab, deathZoneCount, deathZoneRadius);
    }

    void GenerateZones(GameObject zonePrefab, int count, float radius)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position;
            int attempts = 0;

            do
            {
                position = new Vector3(
                    Random.Range(mapMinX + radius + 3f, mapMaxX - radius - 3f),
                    0.5f,
                    Random.Range(mapMinZ + radius + 3f, mapMaxZ - radius - 3f)
                );
                attempts++;
            }
            while (!IsValidPosition(position, radius) && attempts < 100);

            if (attempts < 100)
            {
                zonePositions.Add(position);
                GameObject zone = Instantiate(zonePrefab, position, Quaternion.identity);
                zone.transform.localScale = new Vector3(radius * 2, 1, radius * 2);
            }
            else
            {
                Debug.LogWarning("Failed to place zone after 100 attempts. Consider increasing the map size or decreasing the zone count.");
            }
        }
    }

    bool IsValidPosition(Vector3 position, float radius)
    {
        foreach (Vector3 otherPosition in zonePositions)
        {
            if (Vector3.Distance(position, otherPosition) < radius * 2 + 3f)
            {
                return false;
            }
        }
        return true;
    }
}