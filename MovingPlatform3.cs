using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform3 : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    private IEnumerator stopmoving;
    public bool moving;
    public bool inUse = false;
    private float starTime;

    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(0,-9,0);
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong((Time.time - starTime) * speed, 1)); 
        }
    }

    public void StartMoving()
    {
        speed = 0.3f;
        if (!inUse)
        {
            moving = true;
            inUse = true;
            starTime = Time.time;
        }
        
        stopmoving = StopMoving(20.0f);
        StartCoroutine(stopmoving);
    }

    public IEnumerator StopMoving(float waitTime)
    {
            yield return new WaitForSeconds(waitTime);
            speed = 0.0f;
            moving = false;
            inUse = false;
            
    }
    
}
