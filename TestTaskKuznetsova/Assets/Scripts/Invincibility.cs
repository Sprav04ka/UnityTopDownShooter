using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public float duration = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ApplyInvincibility(other.GetComponent<PlayerHealth>()));
            Destroy(gameObject);
        }
    }

    IEnumerator ApplyInvincibility(PlayerHealth player)
    {
        player.isInvincible = true;
        yield return new WaitForSeconds(duration);
        player.isInvincible = false;
    }
}