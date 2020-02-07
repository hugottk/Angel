using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    
    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
    
    //Tue un ennemi en fonction de ses points de vie max.
    public override void OneShot()
    {
        base.OneShot();
        if (Input.GetKeyDown(KeyCode.D))
        {
            myStats.TakeDamage(myStats.maxHealth + myStats.armor.GetValue());
        }
    }

    //Inflige la moitié des points de vie d'une unité en dégâts.
    public override void Spell()
    {
        base.Spell();
        if (Input.GetKeyDown(KeyCode.R))
        {
            myStats.TakeDamage((myStats.currentHealth + myStats.armor.GetValue()) / 2);
        }
    }
}
