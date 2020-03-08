using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.AI;


public class IAenemy : MonoBehaviour
{
    public NavMeshAgent agent;
    private Vector3 initpos;
    private PhotonView PV;
    GameObject[] enemyTarget;
    private GameObject closest;
    CharacterCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
        initpos = transform.position;
        enemyTarget = new GameObject [4];
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {    
        if (PV.IsMine)
        {
            GameObject target = FindClosestEnemy();
            float playerDistance = Vector3.Distance(target.transform.position, transform.position);

            if (playerDistance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {	
                    combat.Attack(targetStats);
                }
            }
            
            if (Vector3.Distance(closest.transform.position, transform.position) < 10)
            {
                agent.SetDestination(closest.transform.position);
            }
            else
            {
                agent.SetDestination(initpos);
            }
        }
    }
    GameObject FindClosestEnemy()
    {
        enemyTarget = GameObject.FindGameObjectsWithTag("Player");
        float distance = Mathf.Infinity;
     
        foreach (GameObject go in enemyTarget) 
        {
            Vector3 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
         
            if (curDistance < distance) 
            {
                closest = go;
                distance = curDistance;
                //Debug.Log(closest.name);
            }
        }
        return closest;
    }
}