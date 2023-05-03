using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    public float lookRadius= 10f;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Animator animator;
    EnemyStats stats;
    public AudioSource AudioSource;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat= GetComponent<CharacterCombat>();
        animator = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    public void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius || stats.agro)
        {
            agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    combat.Attack(targetStats);

                }
                
                FaceTarget();
            }
        }
        if(agent.velocity.magnitude > 0f)
        {
            animator.SetBool("isMoving", true);
            if(AudioSource != null)
            AudioSource.enabled = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            if(AudioSource != null)
            AudioSource.enabled = false;
        }
    }

    public void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion LookDirection = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,LookDirection, Time.deltaTime*5);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
