using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Interact focus;
    public LayerMask MoveMask;
    Camera watcher;
    PlayerMotor motor;
    public ForceField barrier;
    public Fireball fireball;
    public GameObject fireParticles;
    public Animator animator;
    public bool isCasting=false;
    public AudioSource audioSource;
    


    void Start()
    {
        watcher = Camera.main;
        motor = GetComponent<PlayerMotor>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = watcher.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000, MoveMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = watcher.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.TryGetComponent<Interact>(out var interact))
                {
                    SetFocus(interact);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 180f, 0));
            motor.CancelDestination();
            RemoveFocus();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            motor.CancelDestination();
            RemoveFocus();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            barrier.Cast();
        }
        if(Input.GetKeyDown(KeyCode.F)&& !isCasting)
        {
            if (focus == null||focus.gameObject.GetComponent<EnemyStats>()==null) fireball.EnableLog("No target!");
            else
            {
                motor.CancelDestination();
                StartCoroutine(CastFireballWithDelay());
            }

        }



        if (!motor.inMotion)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -100 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, 100 * Time.deltaTime);
            }
        }

        
    }

    private IEnumerator CastFireballWithDelay()
    {
        float distanceToFocus = Vector3.Distance(transform.position, focus.transform.position);
        if (distanceToFocus <= 3 || distanceToFocus >20)
        {
            fireball.EnableLog("Not in range!");
            yield break;
        }
        isCasting = true;
        animator.SetBool("isCasting", true);
        motor.agent.enabled = false;
        audioSource.Play();
        fireParticles.SetActive(true);
        yield return new WaitForSeconds(1.2f); 
        fireball.Cast(); 
        isCasting=false;
        animator.SetBool("isCasting", false);
        motor.agent.enabled = true;
        fireParticles.SetActive(false); 
    }
    void SetFocus(Interact newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
       

    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        
        focus = null;
        motor.StopFollowingTarget();
    }

}
