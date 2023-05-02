using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public float attackRange = 5.0f;
    public float attackDelay = 3.0f;
    EnemyStats bossStats;
    private float timer;
    public bool debugBool, debugBool2;
    public GameObject attackRangeVisual;
    bool hasIncreasedDamage = false;
    bool hasIncreasedDamageAt75 = false;
    float rand;

    private void Start()
    {
        bossStats = GetComponent<EnemyStats>();
    }
    void Update()
    {
        if (bossStats.currentHealth <= 125)
        {
            if (!hasIncreasedDamage)
            {
                IncreaseDamage();
                hasIncreasedDamage = true;
            }

            if (bossStats.currentHealth <= 100 && !debugBool && !debugBool2)
            {
                StartCoroutine(ShowAttackRange());
                debugBool2 = true;
            }
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                rand = Random.Range(0.0f, 1.0f);

                if (rand <= 0.05f && bossStats.currentHealth <= 100 && !debugBool)
                {
                    StartCoroutine(ShowAttackRange());
                }
            }
            if (bossStats.currentHealth <= 75 && !hasIncreasedDamageAt75)
            {
                IncreaseDamage();
                hasIncreasedDamageAt75 = true;
            }

        }
    }

    void IncreaseDamage()
    {
        bossStats.damage.AddModifier(2);
    }
    IEnumerator ShowAttackRange()
    {

        debugBool = true;
        // Display attack range for 3 seconds
        Debug.DrawRay(transform.position, Vector3.forward * attackRange, Color.red, 3.0f);
        attackRangeVisual.SetActive(true); // Show the attack range sphere
        yield return new WaitForSeconds(3.0f);
        attackRangeVisual.SetActive(false); // Show the attack range sphere

        // Damage player if they are within attack range
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<PlayerStats>(out var player))
            {
                if (bossStats.currentHealth > 50)
                {
                    player.TakeDamage(bossStats.damage.GetValue() * 2);
                }
                else
                {
                    player.TakeDamage(bossStats.damage.GetValue() * 3);
                }

            }
        }

        debugBool = false;
    }
    private void OnDrawGizmosSelected()
    {
        // Draw wire sphere to represent the attack range
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
