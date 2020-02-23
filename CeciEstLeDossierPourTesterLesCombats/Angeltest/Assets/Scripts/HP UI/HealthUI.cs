using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject playerCanvas;
    
    public GameObject uiPrefab;

    public Transform target;
    //float visibleTime = 5;

   //float lastMadeVisibleTime;
    

    Transform ui;

    Transform cam;

    Image HealthSLider;

    Quaternion rotation;
    private void Awake()
    {
        rotation = transform.rotation; //Disable the fact that the healthui rotate with the hero
    }

    void Start()
    {
        //cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, playerCanvas.transform).transform;
                HealthSLider = ui.GetChild(0).GetComponent<Image>();
                //ui.gameObject.SetActive(false);
                break;
            }
        }

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
        if (ui != null)
        {
            ui.position = target.position;
            ui.rotation = rotation; //Disable the fact that the healthui rotate with the hero
            //ui.forward = -cam.forward;

            //if (Time.time - lastMadeVisibleTime > visibleTime)
            //{
            //ui.gameObject.SetActive(false);
            //}
        }
    }
}
