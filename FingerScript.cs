using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerScript : MonoBehaviour
{
    public Quaternion rotation1 = Quaternion.Euler(0, 0, 0);
    public Quaternion rotation2 = Quaternion.Euler(-90, 0, 0);

    private float speed = 0.3f;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(rotation1, rotation2, Mathf.PingPong(Time.time * speed, 1));
    }
}
