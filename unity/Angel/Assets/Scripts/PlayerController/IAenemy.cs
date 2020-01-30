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
    List<GameObject> playerlist;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        agent = GetComponent<NavMeshAgent>();
        initpos = transform.position;
        playerlist = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {    
        if (PV.IsMine)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(initpos, player.transform.position) < 10)
                {
                    if (!playerlist.Any())
                    {
                        playerlist.Append(player);
                        agent.SetDestination(playerlist[0].transform.position);
                    }
                }
                else
                {
                    if (playerlist.Any())
                    {
                        playerlist.RemoveAt(0);
                        onplayerleave(ref playerlist);
                    }
                    agent.SetDestination((initpos));
                }
            }
        }
    }

    void onplayerleave(ref List<GameObject> list)
    {
        int l = list.Count-1;
        while (l>0)
        {
            list[l - 1] = list[l];
            l--;
        }
    }
}