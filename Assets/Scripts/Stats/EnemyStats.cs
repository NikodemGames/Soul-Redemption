using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    Animator animator;
    public GameObject itemDropPrefab; // The prefab of the item to drop
    public float itemDropChance = 0.1f; // The chance of dropping the item (10%)
    public bool agro;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void Die()
    {
        base.Die();
        //Add ragdoll or death anim;
        animator.SetBool("isDead", true);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<CharacterCombat>().enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = false;
        gameObject.GetComponent<Enemy>().enabled = false;
        gameObject.GetComponent<EnemyStats>().enabled = false;
        Destroy(gameObject, 3);
        //loot
        if (Random.Range(1, 11) <= (itemDropChance * 10))
        {
            Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        }
    }
}
