using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Reference the Players health script
    [SerializeField] private PlayerHealth playerHealth;

    // Reference to the zombie script
    [SerializeField] private ZombieAI zombieAI;

    [Header("Health")]
    public float playersCurrentHealth;
    public float lowestHealthReached;
    public float healingUsage;

    [Header("Combat")]
    public float damageTaken;
    public float damageDealt;
    public float totalShots;
    public float shotsHit;
    public float shotsMissed;
    public float accuracy;
    public float reloadCount;

    [Header("Zombies")]
    public float zombiesRemaining;
    public float zombiesSpawned;
    public float zombiesKilled;
    public float zombieHits;
    //public float attacksMissed;
    //public float timeBetweenAttacks;

    // States
    public void Start()
    {
        // On start, lowest health reached is equivalent to the current health
        lowestHealthReached = playerHealth.currentHealth;
    }
    public void Update()
    {
        // Players current Health
        playersCurrentHealth = playerHealth.currentHealth;

        // Track lowest health reached
        if (playersCurrentHealth < lowestHealthReached)
        {
            lowestHealthReached = playersCurrentHealth;
        }

        // Track shots missed
        shotsMissed = totalShots - shotsHit;

        // Calculate shot accuraccy
        if (totalShots > 0)
        {
            accuracy = shotsHit / totalShots * 100;
        }

        // Calculate how many zombies are still remaining
        zombiesRemaining = zombiesSpawned - zombiesKilled;
    }

    public void HealingUsage()
    {
        // Increase amount of times the player has healed
        healingUsage++;
    }

    public void ShotHit()
    {
        // Increase shots hit
        shotsHit++;
    }

    public void TotalShots()
    {
        // Increase total amount of shots
        totalShots++;
    }

    public void DamageDealt(float amount)
    {
        // Increase amount of damage has been dealt
        damageDealt += amount;
    }

    public void DamageTaken(float damage)
    {
        // Increase amount of damage has been taken from
        damageTaken += damage;
    }

    public void ReloadCount()
    {
        // Increase amount of times the player has reloaded
        reloadCount++;
    }

    public void ZombiesSpawned()
    {
        // Increase amount of zombies spawned
        zombiesSpawned++;
    }

    public void ZombiesKilled()
    {
        // Increase the total number of zombies killed
        zombiesKilled++;
    }

    public void ZombieHits()
    {
        // Increase the amount of times a zombie has hit the player
        zombieHits++;
    }
}