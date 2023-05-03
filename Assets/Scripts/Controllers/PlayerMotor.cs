using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    // Start is called before the first frame update
    
    public NavMeshAgent agent;
    Transform target;
    Animator animator;
    public bool inMotion;
    public AudioSource AudioSource;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
        if(agent.velocity.magnitude >0f)
        {
            animator.SetBool("isMoving", true);
            inMotion = true;
            AudioSource.enabled = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            inMotion = false;
            AudioSource.enabled = false;
        }


    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        
    }

    public void FollowTarget(Interact newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        target = null;
        agent.updateRotation = true;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public void CancelDestination()
    {
        agent.destination = transform.position;
    }
}
