using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAI : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyStats>(out var enemyStats))
        {
            enemyStats.TakeDamage(5);
            Destroy(gameObject);
        }
    }
}
