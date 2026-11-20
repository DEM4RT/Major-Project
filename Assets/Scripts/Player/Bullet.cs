using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public int damage = 20;
    
    private void Start()
    {
        // Automatically destroy the bullet after 5 second
        Invoke(nameof(Impact), 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with the player and bullets
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            return;
        }

        // Check if object hit has a zombie health component, if so deal damage
        if (other.TryGetComponent(out ZombieHealth enemy))
        {
            enemy.TakeDamage(damage);
        }

        Impact();
    }

    private void Impact()
    {
        // Remove the bullet from the scene
        Destroy(gameObject);
    }
}
