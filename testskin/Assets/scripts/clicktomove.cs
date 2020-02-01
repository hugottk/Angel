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
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Animatorwings = wings.GetComponent<Animator>();
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
                    player.SetDestination(hit.point);
                    player.isStopped = false;
                }

        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
        
        Animator.SetBool("slash",slash);
        Animatorwings.SetBool("combat",slash);
    }
}
