﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PhotonView PV;

    public NavMeshAgent player;

    public GameObject camera;

    public Interactable focus;

    private Transform target;
    
    private Animator anim;
    private bool running;
    private bool flying;
    private int attackrange = 20;
    private bool Strike = false;
    public GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        player = GetComponent<NavMeshAgent>();
        camera.SetActive(PV.IsMine);
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            //nouvel gameobject : player qui a appuyé sur R (currPlayer)
            Strike = false;
            //if (Input.GetKeyDown(KeyCode.R))
            // currPlayer.GetComponent<PlayerStats>(); (pour avoir les stats du player et surtout le mana
            // currPlayer.GetComponent<Interactable>(); ... (pour savoir si il a un focus ou non (et donc si il peut lancer un sort ou non))
            // faire la diminution du mana si possible comme avant.
            foreach (GameObject attackPlayer in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (attackPlayer.GetComponent<PhotonView>().IsMine)
                {
                    myPlayer = attackPlayer;
                }
            }

            PlayerStats myPlayerStats = myPlayer.GetComponent<PlayerStats>();
            Interactable InteractMyPlayer = myPlayer.GetComponent<Interactable>();

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (myPlayerStats.currentMana >= 10 && focus != null)
                {
                    myPlayerStats.currentMana -= 10;
                }
            }
            
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    RemoveFocus();
                    running = true;
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        if (player.remainingDistance < attackrange)
                        {
                            Strike = true;
                            running = false;
                            flying = false;
                        }
                    }
                    else
                    {
                        Strike = false;
                    }
                    player.SetDestination(hit.point);
                }
            }
            if (AnimatorIsPlaying() && anim.GetCurrentAnimatorStateInfo(0).IsName("combat"))
            {
                player.isStopped = true;
                running = false;
                flying = false;
            }
            else
            {
                player.isStopped = false;
            }
            if (player.remainingDistance <= player.stoppingDistance)
            {
                running = false;
            }
            anim.SetBool("running",running);
            anim.SetBool("flying", flying);
            anim.SetBool("strike",Strike);
            
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
    
    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
