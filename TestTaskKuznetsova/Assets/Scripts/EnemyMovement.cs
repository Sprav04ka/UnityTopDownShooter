using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private Enemy enemy;

    private void Start()
    {
        // Looking for an object with the "Player" tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found!");
        }

        enemy = GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("Enemy component not found!");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * enemy.speed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("Player transform is null!");
        }
    }
}