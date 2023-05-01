using UnityEngine;

public class FireballAI : MonoBehaviour
{
    public float destroyDelay = 4f; // Delay before the fireball is destroyed

    private void Start()
    {
        Destroy(gameObject, destroyDelay); // Destroy the fireball game object after a delay
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyStats>(out var enemyStats))
        {
            enemyStats.TakeDamage(7);
            Destroy(gameObject); // Destroy the fireball game object on collision
        }

       
    }
}
