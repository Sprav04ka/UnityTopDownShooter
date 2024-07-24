using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostMultiplier = 1.5f;
    public float duration = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplySpeedBoost(other.GetComponent<PlayerMovement>()));
            Destroy(gameObject);
        }
    }

    IEnumerator ApplySpeedBoost(PlayerMovement player)
    {
        player.moveSpeed *= boostMultiplier;
        yield return new WaitForSeconds(duration);
        player.moveSpeed /= boostMultiplier;
    }
}