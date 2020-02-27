using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class PlayerStats : CharacterStats
{
    private float t = 0.0f;
    private float Second = 1.0f;
    public event System.Action<int, int> OnManaChanged;
    public override void Die()
    {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        base.Die();
    }

    void Update()
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