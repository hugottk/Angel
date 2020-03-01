using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 4f;

    public bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    public virtual void OneShot()
    {
        
    }

    public virtual void Spell()
    {
        
    }

    void Update()
    {
        //Tue tous les ennemis si on appuie sur D
        OneShot();
        if (isFocus)
        {
            //inflige des dégâts à distance sur le focus
            Spell();
        }
        
        if (!hasInteracted && isFocus)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            float spellDistance = Vector3.Distance(player.position, transform.position);
            if (distance <= radius)
            {
                Interact(); 
                hasInteracted = true; 
            }
            else if (spellDistance <= radius * 10)
            {
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}