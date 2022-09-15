using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float bonusHP = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<HealthManager>().GainHP(bonusHP);
        this.gameObject.SetActive(false);
    }
}
