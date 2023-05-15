using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public float attackRange = 5.0f;
    public float attackDelay = 3.0f;
    EnemyStats bossStats;
    private float timer;
    bool debugBool, debugBool2;
    public GameObject attackRangeVisual;
    bool hasIncreasedDamage = false;
    bool hasIncreasedDamageAt75 = false;
    float rand;
    bool died;
    Animator animator;
    public float cooldown;
    public AudioSource attackSound,deathSound,explosionSound;

    private void Start()
    {
        bossStats = GetComponent<EnemyStats>();
        animator = GetComponent<Animator>();
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
                cooldown = 0;
                debugBool2 = true;

            }
            timer += Time.deltaTime;
            cooldown += Time.deltaTime;
            if (timer >= 1.0f)
            {
                timer = 0.0f;
                rand = Random.Range(0.0f, 1.0f);

                if (rand <= 0.1f && bossStats.currentHealth <= 100 && !debugBool&&bossStats.currentHealth>0&& cooldown >=13)
                {
                    StartCoroutine(ShowAttackRange());
                    cooldown = 0.0f;
                }
            }
            if (bossStats.currentHealth <= 75 && !hasIncreasedDamageAt75)
            {
                IncreaseDamage();
                hasIncreasedDamageAt75 = true;
            }
            if(bossStats.currentHealth <= 0&&!died)
            {
                deathSound.Play();
                died = true;
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
        attackSound.Play();
        // Display attack range for 3 seconds
        Debug.DrawRay(transform.position, Vector3.forward * attackRange, Color.red, 3.0f);
        attackRangeVisual.SetActive(true); // Show the attack range sphere
        animator.SetBool("isAoe",true);
        gameObject.GetComponent<CharacterCombat>().enabled=false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(4f);
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
                else if(bossStats.currentHealth <= 0)
                {
                    player.TakeDamage(bossStats.damage.GetValue() * 0);
                }
                else
                {
                    player.TakeDamage(bossStats.damage.GetValue() * 3);
                }

            }
        }
        explosionSound.Play();
        gameObject.GetComponent<CharacterCombat>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        debugBool = false;
        animator.SetBool("isAoe", false);
    }

}
