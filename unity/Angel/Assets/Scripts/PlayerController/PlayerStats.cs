using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        base.Die();
    }
}