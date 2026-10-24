using Unity.VisualScripting;
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
    public float healingItemUsage;

    [Header("Combat")]
    public float damageTaken;
    public float damageDealt;
    public float shotsHit;
    public float shotsMissed;
    public float accuracy;
    public float timeBetweenEngagement;

    [Header("Zombies")]
    public float stillAlive;
    public float amountSpawned;
    public float amountKilled;
    public float attacksHit;
    public float attacksMissed;
    public float timeBetweenAttacks;
    



    void Start()
    {

    }

    void Update()
    {
        // Players current Health
        playersCurrentHealth = playerHealth.currentHealth;

        // Damage taken by the player
        
    }
}
