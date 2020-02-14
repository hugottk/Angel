using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    CharacterStats myStats;
    public PhotonView PV;
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            
            targetStats.RPCdamage(myStats.damage.GetValue());
            attackCooldown = 1f / attackSpeed;
        }
    }
}