using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Zombie Types")]
    [SerializeField] private GameObject[] zombiePrefab;
    [SerializeField] private float spawnRate = 5;
    [SerializeField] private int zombiesPerWave;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Player")]
    [SerializeField] private Transform player;

    private float spawnTimer;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < zombiesPerWave; i++)
        {
            SpawnZombies();
        }
    }

    private void SpawnZombies()
    {
        // Select a random prefab
        GameObject selectedZombie = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
        
        // Select a random spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Spawn zombie
        GameObject zombie = Instantiate(selectedZombie, spawnPoint.position, spawnPoint.rotation);

        // Assign the player as the target
        ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();

        zombieAI.SetPlayer(player);
    }
}