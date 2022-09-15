using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothScript : MonoBehaviour
{
    public static ToothScript instance;
    public Vector3 closedPos;
    public Vector3 openPos;
    public float speed = 0.3f;
    
    void Start()
    {
        instance = this;
        closedPos = transform.position;
        openPos = transform.position + new Vector3(0,2,0);

        this.gameObject.SetActive(true);
    }

    public IEnumerator OpenTooth()
    {
        float duration = 2;
        float elapsedTime = 0;
        while(elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(closedPos, openPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
    } 
}
