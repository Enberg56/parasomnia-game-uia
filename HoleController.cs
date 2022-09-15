using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
   
    private Animator holeAnimator;
    public GameObject mila;
    public Vector3 milaPos;
    private Transform target;
    private float speed;
    public bool following = false;

    void Start()
    {
        holeAnimator = GetComponent<Animator>();
        speed = 1.0f;
        mila = GameObject.Find("Mila");
    }
    
    void Update()
    {
        milaPos = new Vector3(mila.transform.position.x, transform.position.y, mila.transform.position.z);
        
        if (following)
        {
            transform.position = Vector3.MoveTowards(transform.position, milaPos, speed * Time.deltaTime);
            transform.LookAt(milaPos);
            holeAnimator.SetBool("sneaking", true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        following = true;

    }
}
