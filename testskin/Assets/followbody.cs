using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followbody : MonoBehaviour
{
    public Transform fakeParent;
    Vector3 offset;

    void Start()
    {
        offset = fakeParent.position - transform.position;
    }
 
    void Update () {
        transform.rotation = fakeParent.rotation;
        transform.position = fakeParent.position + offset;
    }
}