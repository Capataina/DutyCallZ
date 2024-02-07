using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask shootingMask;

    public virtual void Fire()
    {
        if (canShoot == true)
        {
            RaycastHit objectHit;

            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out objectHit, 999f, shootingMask);

            if (objectHit.collider.gameObject.layer == LayerMask.NameToLayer("Zombies"))
            {
                var zombie = objectHit.collider.gameObject.GetComponent<Zombie>();
                
                zombie.TakeDamage(damage);
                
            }
            
            timer = fireCooldown;
            canShoot = false;
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