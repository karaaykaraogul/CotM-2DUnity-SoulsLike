using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{    
    public int maxHP = 100;
    int currentHealth;
    public HealthBar healthBar;
    public Dissolve dissolve;
    public EnemyAI gAttack;

    void Start()
    {
        currentHealth = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);     
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            dissolve.DissolveDeath();
            healthBar.enabled = false;
            gAttack.enabled = false;
        }

        if(dissolve.hasDissolved()){
            Die();
        }
    }

    void Die()
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
}
