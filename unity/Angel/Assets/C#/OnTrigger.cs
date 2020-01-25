using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    private Color initialColor;
    void OnTriggerEnter(Collider col)
    {
        initialColor = col.GetComponent<Renderer>().material.color;
        col.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnTriggerExit(Collider col)
    {
        col.GetComponent<Renderer>().material.color = initialColor;
    }
}
