using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 50;
    public int currentHealth { get; private set; }
    public int currentMana { get; set; }
    
    public Stat damage;
    public Stat armor;
    public event System.Action<int, int> OnHealthChanged;
    public event System.Action<int, int> OnManaChanged;

    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    void Update()
    {
    }

    
   public void RPCdamage(int damage)
    {
        transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, damage);
    } 
    [PunRPC]
    public void TakeDamage(int damage)    
    {
        
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + "died");
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}