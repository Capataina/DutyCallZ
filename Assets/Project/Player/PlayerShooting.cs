using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public WeaponsClass gun;
    
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponentInChildren<WeaponsClass>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gun)
        {
            if (gun.automatic)
            {
                if (Input.GetMouseButton(0))
                {
                    gun.Fire();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    gun.Fire();
                }
            
            }
        }
        
    }
}
