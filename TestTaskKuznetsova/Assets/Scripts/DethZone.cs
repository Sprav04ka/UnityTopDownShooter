using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DethZone : MonoBehaviour

{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }
}