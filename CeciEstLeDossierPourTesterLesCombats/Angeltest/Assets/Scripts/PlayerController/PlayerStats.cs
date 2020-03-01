using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;


public class PlayerStats : CharacterStats
{
    private float t = 0.0f;
    private float Second = 1.0f;
    public event System.Action<int, int> OnManaChanged;
    
    
    private PhotonView PV;
    public NavMeshAgent player;

    public void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Die()
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        base.Die();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            t += Time.deltaTime;
            if (t >= Second && currentMana < maxMana)
            {
                t = 0.0f;
                currentMana += 1;
            }

            Debug.Log(currentMana + " Current Mana");
            if (OnManaChanged != null)
            {
                OnManaChanged(maxMana, currentMana);
            }
        }
    }
}