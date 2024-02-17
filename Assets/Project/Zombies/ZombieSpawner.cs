using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float biasFactor;
    public GameObject zombiePrefab; // Assign in the inspector
    public GameObject player; // Assign in the inspector
    private float waveCount = 0;
    private bool waveEnded = false; // Initialize to true to start the first wave immediately
    private float waveCooldown;
    private float zombiesToSpawn;
    private GameObject[] spawnPoints;
    private float timer;

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("ZombieSpawnPoint");
    }

    void Update()
    {
        // Check if there are any zombies left only if the current wave has ended
        if (!waveEnded)
        {
            var zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if (zombies.Length <= 0)
            {
                Debug.Log("Wave ended.");
                PrepareNextWave();
            }
        }
    }

    void PrepareNextWave()
    {
        waveCount++;
        zombiesToSpawn = Mathf.Floor(Mathf.Pow(5 + waveCount, 1.25f));
        waveCooldown = zombiesToSpawn / 2;
        waveCooldown = Mathf.Clamp(waveCooldown, 5f, 20f);

        if (spawnPoints.Length > 0)
        {
            waveEnded = true; // Prevent new wave from being prepared again until this one has fully spawned
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveCooldown);

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            SpawnZombie();
            if (i < zombiesToSpawn - 1)
            {
                yield return new WaitForSeconds(0.5f); // Adjust spawn rate as needed
            }
        }

        Debug.Log("Wave Spawning completed for wave number: " + waveCount);
        Debug.Log("Total zombies spawned is: " + zombiesToSpawn);
        waveEnded = false; // Allow the next wave to be prepared once all zombies are dead
    }

    // void SpawnZombie()
    // {
    //     // Calculate the distance from each spawn point to the player and store them
    //     List<float> distances = new List<float>();
    //     foreach (GameObject spawnPoint in spawnPoints)
    //     {
    //         float distance = Vector3.Distance(player.transform.position, spawnPoint.transform.position);
    //         distances.Add(distance);
    //     }
    //
    //     // Convert distances to weights, smaller distances should result in higher weights
    //     // This can be adjusted based on your game's needs, for example using 1/distance as a basic approach
    //     List<float> weights = distances.Select(distance => 1 / distance).ToList();
    //
    //     // Normalize weights to get probabilities
    //     float totalWeight = weights.Sum();
    //     List<float> probabilities = weights.Select(weight => weight / totalWeight).ToList();
    //
    //     // Select a spawn point based on these probabilities
    //     float randomPoint = Random.value;
    //     float currentSum = 0;
    //     int selectedIndex = 0;
    //     for (int i = 0; i < probabilities.Count; i++)
    //     {
    //         currentSum += probabilities[i];
    //         if (randomPoint <= currentSum)
    //         {
    //             selectedIndex = i;
    //             break;
    //         }
    //     }
    //
    //     // Spawn the zombie at the selected spawn point
    //     GameObject zombieSpawnPoint = spawnPoints[selectedIndex];
    //     Instantiate(zombiePrefab, zombieSpawnPoint.transform.position, Quaternion.identity);
    // }

    void SpawnZombie()
    {
        List<float> distances = spawnPoints.Select(spawnPoint => Vector3.Distance(player.transform.position, spawnPoint.transform.position)).ToList();
        float maxDistance = distances.Max();
        List<float> normalizedDistances = distances.Select(distance => distance / maxDistance).ToList(); // Normalize distances

        // Adjust weights with bias factor - use Mathf.Pow to apply the bias based on distance
        List<float> weights = normalizedDistances.Select(distance => Mathf.Pow(Mathf.Exp(1), biasFactor * distance)).ToList();

        // Weighted random selection based on adjusted weights
        float totalWeight = weights.Sum();
        float randomValue = Random.Range(0, totalWeight);
        float currentSum = 0;
        int selectedIndex = -1;
        for (int i = 0; i < weights.Count; i++)
        {
            currentSum += weights[i];
            if (randomValue <= currentSum)
            {
                selectedIndex = i;
                break;
            }
        }

        if (selectedIndex != -1)
        {
            GameObject spawnPoint = spawnPoints[selectedIndex];
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.transform.position, Quaternion.identity);
            zombie.GetComponent<Zombie>().AdjustHealthAndArmor((int)waveCount);
        }
        else
        {
            Debug.LogError("Failed to select a zombie spawn point based on proximity weighting.");
        }
    }

    
}
