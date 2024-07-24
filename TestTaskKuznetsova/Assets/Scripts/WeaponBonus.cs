using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBonus : MonoBehaviour
{
    public int weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Shooting playerShooting = other.GetComponent<Shooting>();
            if (playerShooting != null)
            {
                playerShooting.ChangeWeapon(weaponType);
                Destroy(gameObject); 
            }
        }
    }
}