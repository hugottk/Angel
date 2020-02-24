using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        transform.GetComponent<PhotonView>().RPC("Die2", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public  void Die2()
    {
        base.Die();
        Destroy(gameObject);
        foreach (GameObject child in transform) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}