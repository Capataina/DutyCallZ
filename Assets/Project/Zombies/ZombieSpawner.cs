using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    [SerializeField] public GameObject zombie;
    [SerializeField] public Transform spawnPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(zombie, spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
