using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    private StatsManager stats;

    [Header ("Health Settings")]
    public float maxHealth = 100;
    public float currentHealth;

    private void Awake()
    {
        // Find the stat manager gameobject
        stats = FindAnyObjectByType<StatsManager>();
    }

    private void Start()
    {
        // Set the current health to 100
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        // Decrease the current health by amount of damage dealt
        currentHealth -= amount;
        
        // Call shots hit method
        stats.ShotHit();
        // Call damage dealt method 
        stats.DamageDealt(amount);

        // If the current health is 0, run die to remove the zombie from the scene
        if (currentHealth <= 0)
        {
            // Call zombies killed method
            stats.ZombiesKilled();
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Zombie is dead!");
        // Remove the zombie from the scene
        Destroy(gameObject);
    }
}
