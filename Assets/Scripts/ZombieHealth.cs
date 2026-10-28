using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Zombie is dead!");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }
}
