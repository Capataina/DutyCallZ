using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class WeaponsClass : MonoBehaviour
{
    private float timer;
    private bool canShoot;
    private bool isReloading;
    public float fireCooldown;
    public float reloadTimer;
    public float damage;
    public float magazineSize;
    private float bulletsInMag;
    public float maxAmmo;
    private float currentAmmo;
    public float burstNumber;
    public float burstDelay;
    public float accuracy;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask shootingMask;

    public virtual void Fire()
    {
        if (canShoot && !isReloading && bulletsInMag > 0)
        {
            StartCoroutine(FireBurst());

            if (burstDelay == 0)
            {
                bulletsInMag -= 1;
            }
            
            timer = fireCooldown;
            canShoot = false;
        }
        else if (bulletsInMag <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < burstNumber; i++)
        {
            Shoot();

            if (bulletsInMag <= 0)
            {
                StartCoroutine(Reload());
                yield break; // Stop the burst if we run out of bullets
            }

            if (i < burstNumber - 1)
            {
                yield return new WaitForSeconds(burstDelay);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
        
        if (burstDelay != 0)
        {
            bulletsInMag -= 1;
        }
        
        Debug.Log(bulletsInMag);
        
        float weaponSpread = 1 - accuracy;
        Vector3 accuracyOffset = new Vector3(Random.Range(-1f,1f) * weaponSpread, Random.Range(-1f,1f) * weaponSpread, Random.Range(-1f,1f) * weaponSpread);
        
        if (Physics.Raycast(playerCamera.transform.position, Vector3.Normalize(playerCamera.transform.forward + accuracyOffset), out var objectHit, 999f, shootingMask))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.position + Vector3.Normalize(playerCamera.transform.forward + accuracyOffset) * 999f, Color.green,5);
            if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Zombies"))
            {
                var zombie = objectHit.collider.gameObject.GetComponent<Zombie>();
                zombie.TakeDamage(damage);
            }
        }
    }

    protected virtual IEnumerator Reload()
    {
        if (isReloading || currentAmmo <= 0) yield break; // Prevent multiple reloads or reloading with no ammo

        isReloading = true;
        canShoot = false;
        //Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTimer);

        float bulletsToReload = magazineSize - bulletsInMag;
        
        //Debug.Log("Bullets in mag before reload:" + bulletsInMag);
        //Debug.Log("Total ammo before reload:" + currentAmmo);
        
        if (currentAmmo >= bulletsToReload)
        {
            bulletsInMag += bulletsToReload;
            currentAmmo -= bulletsToReload;
        }
        else
        {
            bulletsInMag += currentAmmo;
            currentAmmo = 0;
        }

        //Debug.Log("Bullets in mag after reload:" + bulletsInMag);
        //Debug.Log("Total ammo after reload:" + currentAmmo);
        
        isReloading = false;
        canShoot = true;
        //Debug.Log("Reload Complete");
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
        bulletsInMag = magazineSize;
    }

    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else if (!isReloading)
        {
            canShoot = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && bulletsInMag < magazineSize)
        {
            StartCoroutine(Reload());
        }
    }
}