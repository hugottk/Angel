using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.AI;


public class IAenemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 initpos;
    private PhotonView PV;


    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
        initpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            foreach (GameObject cur in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(initpos, cur.transform.position) < 500)
                {
                    agent.SetDestination(cur.transform.position);
                }
                else
                {
                    Debug.Log("moving toward init pos");
                    agent.SetDestination((initpos));
                }
            }
        }
    }
}