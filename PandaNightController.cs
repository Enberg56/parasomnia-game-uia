using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaNightController : MonoBehaviour
{

    private Animator PandaAnimator;
    private Rigidbody rigidBody;

    [Header("Animator Bools")]
    public GameObject mila;
    public Vector3 milaPos;
    private Transform target;
    private float speed;
    
    private bool findMila1;
    private bool findMila2;
    private Vector3 adjustRaycast = new Vector3( 0, 1, 0);
    public float visionRange = 4;

    public bool following = false;
    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        PandaAnimator = GetComponent<Animator>();
        speed = 1.0f;
        mila = GameObject.Find("Mila");
    }

    void Update()
    {
        milaPos = new Vector3(mila.transform.position.x, transform.position.y, mila.transform.position.z);
        PandaAnimator.SetBool("isWalking", false);
        
        if (findMila1||findMila2)
        {
            transform.position = Vector3.MoveTowards(transform.position, milaPos, speed * Time.deltaTime);
            transform.LookAt(milaPos);
            PandaAnimator.SetBool("isWalking", true);
        }

        findMila1 = Physics.Raycast(transform.position + adjustRaycast, Vector3.forward, visionRange);
        findMila2 = Physics.Raycast(transform.position + adjustRaycast, Vector3.back, visionRange);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        following = true;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + adjustRaycast , transform.position + adjustRaycast + Vector3.forward * visionRange);
        Gizmos.DrawLine(transform.position + adjustRaycast , transform.position + adjustRaycast + Vector3.back * visionRange);

    }
}