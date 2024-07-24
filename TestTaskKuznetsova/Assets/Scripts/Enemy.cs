using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int scoreValue = 7; 

   
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

  
    private void Die()
    {
        // Add points to ScoreManager when an enemy dies
        ScoreManager.instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
