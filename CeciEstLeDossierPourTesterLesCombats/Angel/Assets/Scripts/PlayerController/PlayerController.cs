using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PhotonView PV;

    public NavMeshAgent player;

    public GameObject camera;

    public Interactable focus;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        player = GetComponent<NavMeshAgent>();
        camera.SetActive(PV.IsMine);
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    RemoveFocus();

                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                    player.SetDestination(hit.point);
                }
            }
            
        }
    }
    
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
            
            focus = newFocus;
            FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
        
    }
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();
        
        focus = null;
        StopFollowingTarget();
    }
    
    public void FollowTarget(Interactable newTarget)
    {
        player.stoppingDistance = newTarget.radius * .8f;
        target = newTarget.transform;
        
    }

    public void StopFollowingTarget()
    {
        player.stoppingDistance = 0f;
        target = null;
    }
}
