using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    public PhotonView PV;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    
    private Animator anim;
    private bool strike;
    private bool running;
    private bool flying;
    private NavMeshAgent player;
    
    void Update()
    {
        attackCooldown -= Time.deltaTime;
        
    }

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        anim = GetComponent<Animator>();
        player = GetComponent<NavMeshAgent>();
    }

    public void Attack(CharacterStats targetStats)
    {
        player.transform.LookAt(targetStats.transform);
        if (attackCooldown <= 0f)
        {
            strike = true;
            flying = false;
            running = false;
            targetStats.RPCdamage(myStats.damage.GetValue());
            attackCooldown = 1f / attackSpeed;
        }
        else
        {
            strike = false;
        }
    }
}