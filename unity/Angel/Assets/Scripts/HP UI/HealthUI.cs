using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{ 
    public GameObject uiPrefab;

    public Transform target;
    //float visibleTime = 5;

   //float lastMadeVisibleTime;


   private Transform ui;

    Transform cam;
    private Quaternion rotation;

    Image HealthSLider;
    void Start()
    {
        
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                HealthSLider = ui.GetChild(0).GetComponent<Image>();
                //ui.gameObject.SetActive(false);
                break;
            }
        }

        rotation = ui.rotation;

        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            //lastMadeVisibleTime = Time.time;
            
            float healthPercent = currentHealth / (float) maxHealth;
            HealthSLider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
    
    void LateUpdate()
    {
        ui.rotation = rotation;
        if (ui != null)
        {
            ui.position = target.position;
            ui.LookAt(cam.position);
            //ui.forward = -cam.forward;

            //if (Time.time - lastMadeVisibleTime > visibleTime)
            //{
                //ui.gameObject.SetActive(false);
            //}
        }
    }
}
