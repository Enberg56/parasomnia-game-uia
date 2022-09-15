using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltLosOjosNightController : MonoBehaviour
{
    private Animator LosOjosNightAnimator;
    private Rigidbody rigidBody;
    private bool following; 

    [Header("Animator Bools")]
    public GameObject mila;
    public Vector3 milaPos;
    private Transform target;
    private float speed;
    
    [Header("Growing")]

    public float maxSize;
    public float growFactor;
    public float waitTime;
    private bool growing;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        LosOjosNightAnimator = GetComponent<Animator>();
        NormalFreeze();
        speed = 1.0f;
        mila = GameObject.Find("Mila");
        following = false;
        growing = false;

    }

    void Update()
    {
        milaPos = new Vector3(mila.transform.position.x, mila.transform.position.y, mila.transform.position.z);
        
        if (following)
        {
            transform.position = Vector3.MoveTowards(transform.position, milaPos, speed * Time.deltaTime);
            transform.LookAt(milaPos);
            LosOjosNightAnimator.SetBool("flying", true);
        }

        if (following && !growing)
        {
            StartCoroutine(StartGrowing(true));
            growing = true;
        }
    }

    void NormalFreeze()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    private void OnTriggerEnter(Collider other)
    {
        following = true;
    }

    public IEnumerator StartGrowing(bool growing)
     {
         float timer = 0;
 
         while(growing == true) 
         {
             
             
             while(maxSize > transform.localScale.x)
             {
                 timer += Time.deltaTime;
                 transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                 yield return null;
             }
             
 
             yield return new WaitForSeconds(waitTime);
 
             timer = 0;
             while(1 < transform.localScale.x)
             {
                 growing = false;
                 yield return null;
             }
 
             timer = 0;
             yield return new WaitForSeconds(waitTime);
             
         }
     }
}

