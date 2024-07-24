using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    public int damage;
    public float speed;
    public float maxDistance = Mathf.Infinity;
    public string shooterTag;

    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(shooterTag) || other.CompareTag("Bullet"))
        {
            return; // Ignore collisions with our own bullets and objects with the Bullet tag
        }
        else if (other.gameObject.CompareTag("Zone")| other.gameObject.CompareTag("Invincibility") | other.gameObject.CompareTag("SpeedBoost") | other.gameObject.CompareTag("WeaponBonus"))
        {
           return; // Ignore collisions with zones and bonuses
        }
         else if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy the bullet when it collides with other objects


        }
            
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zone"))
        {
            return; 
        }
        else if (!collision.gameObject.CompareTag("WeaponBonus"))
        {
            return; 
        }
        else if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile collided with: " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }*/
}