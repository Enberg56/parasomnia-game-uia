using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorScript : MonoBehaviour
{

    public static TrapdoorScript instance;
    public Vector3 closedPos;
    public Vector3 openPos;
    public float speed = 0.3f;

    void Start()
    {
        instance = this;
        closedPos = transform.position;
        openPos = transform.position + new Vector3(0,3,0);
    }
    
    public IEnumerator OpenDoor()
    {
        float duration = 2;
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(closedPos, openPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        
        yield return new WaitForSeconds(60.0f);
        elapsedTime = 0;

        while(elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(openPos, closedPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
    }
}
