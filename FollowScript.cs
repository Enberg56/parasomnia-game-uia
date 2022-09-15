using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowScript : MonoBehaviour
{
    public Transform mila;
    public float speed = 0.1f;
    private Vector3 targetDirection;

    public Vector3 startScale;
    public Vector3 endScale;
    public float size = 0.1f;

    private MilaController milaController;
    private AltLosOjosNightController altLosOjosNightController;
    private Transform thisObject;
    
    public bool startFollowing;
    public bool startGrowing;
    public bool following;
    public bool milaMoving;

    void Start()
    {
        milaController = GetComponent<MilaController>();
        altLosOjosNightController = GetComponent<AltLosOjosNightController>();
        thisObject = this.transform;

        startFollowing = false;
        startGrowing = false;
        following = false;
        milaMoving = false;

        startScale = transform.localScale;
        endScale = transform.localScale + new Vector3(10,10,10);
    }

    void FixedUpdate()
    {
        
        if(startFollowing && !following)
        {
            FollowMila();
        }

        if(startGrowing && following)
        {
            Growing();
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            milaMoving = true;
        }
        else
        {
            milaMoving = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        startFollowing = true;
    }

    public void FollowMila()
    {
        following = true;
        while(following)
        {
            targetDirection = mila.transform.position - transform.position;
            transform.LookAt(mila);
            targetDirection = targetDirection.normalized;
            transform.Translate(targetDirection * speed, Space.World);
        }
    }

    public IEnumerator Growing()
    {
        float growSpeed = 0.1f;
        while(milaMoving)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, growSpeed);
        }

        while(!milaMoving)
        {
            transform.localScale = Vector3.Lerp(endScale, startScale, growSpeed);
        }

        return null;
    }
}



