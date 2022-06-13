using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{    
    public int maxHP = 100;
    int currentHealth;
    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
