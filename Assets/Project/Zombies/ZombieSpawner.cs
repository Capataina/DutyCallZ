using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private float biasFactor;
    public GameObject zombiePrefab; // Assign in the inspector
    GameObject player; // Assign in the inspector
    private float waveCount;
    private bool waveEnded; // Initialize to true to start the first wave immediately
    private float waveCooldown;
    private float zombiesToSpawn;
    private GameObject[] spawnPoints;
    private float zombiesSpawned;
    [HideInInspector] public float zombiesKilled;
    [HideInInspector] public static ZombieSpawner current;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("ZombieSpawnPoint");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Check if there are any zombies left only if the current wave has ended
        if (!waveEnded)
        {
            print(zombiesKilled + " killed, target: " + zombiesSpawned);
            if (zombiesKilled == zombiesSpawned)
            {
                PrepareNextWave();
            }
        }
    }

    void PrepareNextWave()
    {
        zombiesKilled = 0;
        zombiesSpawned = 0;
        waveCount++;
        zombiesToSpawn = Mathf.Floor(Mathf.Pow(5 + waveCount, 1.25f));
        waveCooldown = zombiesToSpawn / 2;
        waveCooldown = Mathf.Clamp(waveCooldown, 5f, 20f);
        // DEBUGGING
        waveCooldown = 1;

        if (spawnPoints.Length > 0)
        {
            waveEnded = true; // Prevent new wave from being prepared again until this one has fully spawned
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveCooldown);

        zombiesSpawned = zombiesToSpawn;
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
            Zombie component = zombie.GetComponent<Zombie>();
            component.AdjustHealthAndArmor((int)waveCount);
        }
        else
        {
            Debug.LogError("Failed to select a zombie spawn point based on proximity weighting.");
        }
    }


}
