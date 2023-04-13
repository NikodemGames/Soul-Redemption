using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject shield;

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
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = watcher.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000, MoveMask))
            {
                motor.MoveToPoint(hit.point);
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = watcher.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Interact interact = hit.collider.GetComponent<Interact>();
                if(interact != null)
                {
                    SetFocus(interact);
                }

            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForceField();
        }
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

    void ForceField()
    {
        if(shield.GetComponent<ForceField>())
        {
            shield.gameObject.SetActive(true);
        }
    }
}
