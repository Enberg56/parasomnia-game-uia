using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public int s = 1;

    private void OnTriggerEnter(Collider other)
    {
        
        other.GetComponent<HealthManager>().AddPoints(s);
        this.gameObject.SetActive(false);

    }
}
