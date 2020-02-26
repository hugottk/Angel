using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class ManaUI : MonoBehaviour
{
    public GameObject playerCanvas;
    
    public GameObject uiManaPrefab;

    public Transform target;

    Transform uiMana;

    Transform cam;

    Image ManaSlider;

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
                
                uiMana = Instantiate(uiManaPrefab, playerCanvas.transform).transform;
                ManaSlider = uiMana.GetChild(0).GetComponent<Image>();
                break;
            }
        }

        GetComponent<CharacterStats>().OnManaChanged += OnManaChange;
    }

    void OnManaChange(int maxMana, int currentMana)
    {
        if (uiMana != null)
        {
            uiMana.gameObject.SetActive(true);
            float manaPercent = currentMana / (float) maxMana;
            ManaSlider.fillAmount = manaPercent;

        }
    }
    
    
    void LateUpdate()
    {
        if (uiMana != null)
        {
            uiMana.position = target.position;
            uiMana.rotation = rotation;    
        }
        GetComponent<CharacterStats>().OnManaChanged += OnManaChange;
    }
}
