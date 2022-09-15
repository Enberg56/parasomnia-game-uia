using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonfishScript : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    public Quaternion rotation1 = Quaternion.Euler(270, -90, -90);
    public Quaternion rotation2 = Quaternion.Euler(270, 90, -90);
    
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position + new Vector3(0,0,-26);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1));

        if(transform.position.z <= 57)
        {
            transform.rotation = rotation2;
        }
        else if(transform.position.z >= 83)
        {
            transform.rotation = rotation1;
        }

    }
    private void OnCollisionEnter(Collision collision){
        Vector3 force = collision.contacts[0].normal;
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.AddForce((-force * 200)+(Vector3.up * 250));
        HealthManager.instance.TakeDamage(1.0f);
    }
}
