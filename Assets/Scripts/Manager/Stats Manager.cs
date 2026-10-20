using Unity.VisualScripting;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Reference the "Player Health" script
    [SerializeField] private PlayerHealth playerHealth;

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
    }
}
