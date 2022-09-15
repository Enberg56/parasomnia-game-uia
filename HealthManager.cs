using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

     [Header("Score & health")]
    public TextMeshProUGUI scoreText;
    public int playerPoints;
    public TextMeshProUGUI livesText;
    public Slider healthBar;
    private float health;
    private float currentHealth;
    
    void Start()
    {
        instance = this;
        health = 12.0f;
        currentHealth = health;
        playerPoints = 0;
        SetScoreText();
        SetLivesText();
    }

    void Update()
    {
        
    }

    public void AddPoints(int s)
    {
        playerPoints += s;
        SetScoreText();
    }

    public void SetScoreText()
    {
        scoreText.text = "Score: " + playerPoints;
    }

    public void SetLivesText()
    {
        livesText.text = "Lives: " + currentHealth;
    }

    public int GetCurrentPoints()
    {
        return playerPoints;
    }

    public float UpdateCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = healthBar.value + damage;

        if (currentHealth <= 0.0f)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (currentHealth <= 6.0f)
        {
            MilaController.instance.ChangeScene(currentHealth);
        }
        SetLivesText();

    }

    public void GainHP(float bonusHP)
    {
        if(currentHealth >= 12)
        {
            int s = (int) bonusHP;
            AddPoints(s);
        }
        else{
            currentHealth += bonusHP;
            healthBar.value = healthBar.value - bonusHP;

            if (currentHealth >= 7.0f)
            {
            MilaController.instance.ChangeScene(currentHealth);
             }
            SetLivesText();
        }
        
    }
    
}
