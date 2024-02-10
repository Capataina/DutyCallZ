using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    
    public GameObject zombiePrefab; // Assign in the inspector
    public float spawnInterval = 5f; // Time between each spawn

    private GameObject[] spawnPoints;
    private float timer;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("ZombieSpawnPoint");
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && spawnPoints.Length > 0)
        {
            SpawnZombie();
            timer = spawnInterval;
        }
    }

    void SpawnZombie()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[spawnIndex];
        Instantiate(zombiePrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }
    
}
