using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCD = 0f;
    public float attackDelay = .6f;
    public event System.Action OnAttack;
    CharacterStats myStats;
    private void Start()
    {
        myStats= GetComponent<CharacterStats>();
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
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
    }
}
