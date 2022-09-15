using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour
{
    private float speed = 10.0f;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0);
    }
}
