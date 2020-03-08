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

    Transform ui;

    Image HealthSLider;

    Quaternion rotation;
    private void Awake()
    {
        rotation = transform.rotation; //Disable the fact that the healthui rotate with the hero
    }

    void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, playerCanvas.transform).transform;
                HealthSLider = ui.GetChild(0).GetComponent<Image>();
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
        }
    }
}
