using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    CharacterStats myStats;
    
    GameObject[] enemyTarget;

    void Start()
    {
        enemyTarget = new GameObject [4];
        myStats = GetComponent<CharacterStats>();
    }
    
    public override void Interact()
    {
        base.Interact();
        enemyTarget = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject go in enemyTarget)
        {
            CharacterCombat playerCombat = go.GetComponent<CharacterCombat>();
            if (playerCombat != null)
            {
                playerCombat.Attack(myStats);
            }
        }
        
    }
    
    //Tue un ennemi en fonction de ses points de vie max.
    public override void OneShot()
    {
        base.OneShot();
        if (Input.GetKeyDown(KeyCode.D))
        {
            myStats.RPCdamage(myStats.maxHealth + myStats.armor.GetValue());
        }
    }

    //Inflige la moitié des points de vie d'une unité en dégâts.
    public override void Spell()
    {
        base.Spell();
        if (Input.GetKeyDown(KeyCode.R))
        {
            enemyTarget = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject go in enemyTarget)
            {
                CharacterStats playerMana = go.GetComponent<CharacterStats>();
                if (playerMana.currentMana >= 10)
                {
                    playerMana.currentMana -= 10;
                    myStats.RPCdamage(10);
                }
            }
            
        }
    }
}