﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
        initpos = transform.position;
        enemyTarget = new GameObject [4];
    }

    // Update is called once per frame
    void Update()
    {    
        if (PV.IsMine)
        {
            agent.SetDestination(FindClosestEnemy().transform.position);
        }
    }
    GameObject FindClosestEnemy()
    {
        enemyTarget = GameObject.FindGameObjectsWithTag("Player");
        float distance = Mathf.Infinity;
     
        foreach (GameObject go in enemyTarget) 
        {
            Vector3 diff = go.transform.position - initpos;
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