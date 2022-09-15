using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Player")]
    public MilaController milaController;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    void Update()
    {
        
    }

    public void SetStartHealth()
    {
        
    }
}
