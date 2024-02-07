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

    private void Shoot()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var objectHit, 999f, shootingMask))
        {
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