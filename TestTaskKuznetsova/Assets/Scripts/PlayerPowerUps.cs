using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour

{
    private PlayerMovement playerMovement;
    private Shooting playerShooting;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<Shooting>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Invincibility"))
        {
            StartCoroutine(Invincibility());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedBoost()
    {
        float originalSpeed = playerMovement.moveSpeed;
        playerMovement.moveSpeed *= 1.5f;
        yield return new WaitForSeconds(10f);
        playerMovement.moveSpeed = originalSpeed;
    }

    private IEnumerator Invincibility()
    {
        playerHealth.isInvincible = true;
        yield return new WaitForSeconds(10f);
        playerHealth.isInvincible = false;
    }
}