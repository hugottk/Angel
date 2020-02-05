using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class clicktomove : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent player;
    public GameObject wings;
    private bool slash;
    private Animator Animator;
    private Animator Animatorwings;

    public Interactable focus;

    private Transform target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        slash = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButton(1))
        {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    RemoveFocus();
                    
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                        
                    player.SetDestination(hit.point);
                    player.isStopped = false;
                }
                

        }

        if (target != null)
        {
            player.SetDestination(target.position);
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            player.speed = 10;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {


                player.SetDestination(hit.point);
                slash = true;
                player.isStopped = true;
                transform.LookAt(hit.point);
            }
        }
        else
        {
            slash = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.speed = 15;
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
            
            focus = newFocus;
            FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();
        
        focus = null;
        StopFollowingTarget();
    }

    public void FollowTarget(Interactable newTarget)
    {
        player.stoppingDistance = newTarget.radius * .8f;
        target = newTarget.transform;
        
    }

    public void StopFollowingTarget()
    {
        player.stoppingDistance = 0f;
        target = null;
    }

    public void MoveToPoint(Vector3 point)
    {
        player.SetDestination(point);
    }
}
