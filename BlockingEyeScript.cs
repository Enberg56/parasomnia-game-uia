using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingEyeScript : MonoBehaviour
{
    public static BlockingEyeScript instance;
    void Start()
    {
        instance = this;
        this.gameObject.SetActive(true);
    }

    public void DestroyPlatform()
    {
        this.gameObject.SetActive(false);
    } 
    
    public void RespawnPlatfrom()
    {
        this.gameObject.SetActive(true);
    } 
}
