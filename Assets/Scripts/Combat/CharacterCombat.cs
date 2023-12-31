using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCD = 0f;
    public float attackDelay = .6f;
    public event System.Action OnAttack;
    CharacterStats myStats;
    Animator animator;
    public AudioSource audioSource;
    private void Start()
    {
        myStats= GetComponent<CharacterStats>();
        animator= GetComponent<Animator>();

    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats)
    {
        if(attackCD<=0)
        {
            
            StartCoroutine(DoDamage(targetStats,attackDelay));
            if(OnAttack!= null)
            {
                OnAttack();
            }
            attackCD = 1f/attackSpeed;
        }
        
    }
    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
        animator.SetBool("isAttack", false);
    }
}
