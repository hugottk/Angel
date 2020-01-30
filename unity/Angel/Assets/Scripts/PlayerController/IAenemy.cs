using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.AI;


public class IAenemy : MonoBehaviour
{
    public NavMeshAgent agent;
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
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(initpos, player.transform.position) < 20)
                {
                    agent.SetDestination(player.transform.position);
                    Debug.Log(player.transform.position);
                }
                else
                {
                    
                    agent.SetDestination((initpos));
                }
            }
        }
    }
}