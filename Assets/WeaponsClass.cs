using System.Collections;
using UnityEngine;

public abstract class WeaponsClass : MonoBehaviour
{
    private float timer;
    private bool canShoot;
    public float fireCooldown;
    public float reloadSpeed;
    public float damage;
    public float magazineSize;
    public float maxAmmo;
    public float burstNumber;
    public float burstDelay;
    public float accuracy;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask shootingMask;

    public virtual void Fire()
    {
        if (canShoot)
        {
            StartCoroutine(FireBurst()); 
            timer = fireCooldown; 
            canShoot = false;
        }
    }

    private IEnumerator FireBurst()
    {
        for (int i = 0; i < burstNumber; i++)
        {
            Shoot();

            if (i < burstNumber - 1)
            {
                yield return new WaitForSeconds(burstDelay);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Shoot()
    {
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

    public abstract void Reload();

    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canShoot = true;
        }
    }
}