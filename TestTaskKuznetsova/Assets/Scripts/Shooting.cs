using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour

{
    public GameObject bulletPrefab;
    public GameObject grenadePrefab; 
    public Transform firePoint;
    public float pistolFireRate = 0.5f;
    public float rifleFireRate = 0.1f;
    public float shotgunFireRate = 0.67f; 
    public float grenadeFireRate = 1.5f; 
    public int pistolDamage = 3;
    public int rifleDamage = 1;
    public int shotgunDamage = 2;
    public int grenadeDamage = 10;
    public float bulletSpeed = 20f;
    public float shotgunSpreadAngle = 10f;
    public int shotgunPellets = 5;


    private float nextFireTime = 0f;
    public int selectedWeapon = 1; // Default pistol

    void Start()
    {
        ChangeWeapon(1);
    }

    void Update()
    {
        /* //Uncomment for easier testing
         if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(4);
        }*/

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            switch (selectedWeapon)
            {
                case 1: // Pistol
                    nextFireTime = Time.time + pistolFireRate;
                    Shoot(pistolDamage, bulletSpeed);
                    break;
                case 2: // Rifle
                    nextFireTime = Time.time + rifleFireRate;
                    Shoot(rifleDamage, bulletSpeed);
                    break;
                case 3: // Shotgun
                    nextFireTime = Time.time + shotgunFireRate;
                    ShootShotgun();
                    break;
                case 4: // Grenade
                    nextFireTime = Time.time + grenadeFireRate;
                    ShootGrenade();
                    break;
            }
        }
    }

    void Shoot(int damage, float speed)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.damage = damage;
            bulletComponent.speed = speed;
            bulletComponent.shooterTag = "Player"; // to avoid collision with the player

            // bullet's Rigidbody gets initial velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * speed;
            }
        }
    }

    void ShootShotgun()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            for (int i = 0; i < shotgunPellets; i++)
            {
                float angle = Random.Range(-shotgunSpreadAngle / 2, shotgunSpreadAngle / 2);
                Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * rotation);
                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                bulletComponent.damage = shotgunDamage;
                bulletComponent.speed = bulletSpeed;
                bulletComponent.maxDistance = 7f;
                bulletComponent.shooterTag = "Player"; 

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = firePoint.forward * bulletSpeed;
                }
            }
        }
    }


    void ShootGrenade()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            GameObject grenade = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);
            Grenade grenadeComponent = grenade.GetComponent<Grenade>();
            if (grenadeComponent != null)
            {
                grenadeComponent.damage = grenadeDamage;
                grenadeComponent.targetPosition = GetMouseWorldPosition();
            }
            else
            {
                Debug.LogError("Grenade component not found on grenade prefab.");
            }
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);
        }
        return Vector3.zero;
    }

    public void ChangeWeapon(int weaponType)
    {
        selectedWeapon = weaponType;
        Debug.Log("weaponType: "+ weaponType);
    }
    

}