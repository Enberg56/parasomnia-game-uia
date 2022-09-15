using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraFollow : MonoBehaviour
{
    public Transform camTarget;
    public Vector3 camOffset;
    
    void Start()
    {
        camOffset = transform.position - camTarget.transform.position;
    }

    void Update()
    {
        transform.position = camTarget.position + camOffset;

    }
            
}

