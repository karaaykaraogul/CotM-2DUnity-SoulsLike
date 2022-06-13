using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100;
    int currentHealth;
    public AIPath ai;
    public AIDestinationSetter aid;
    public EnemyPathfindRotation epr;
    public CameraFollow camf;
    public Animator anim;
    public Dissolve dissolve;
    GameObject player;
    public HealthBar healthBar;
    public GameObject DeathScreen;
    private int potions;
    public Text potionCount;

    void Start()
    {
        currentHealth = maxHP;
        player = GameObject.Find("karakter");
        healthBar.SetMaxHealth(maxHP);
        potions = 3;
        potionCount.text = potions.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            
            dissolve.DissolveDeath();
            DeathScreen.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Heal"))
        {
            Heal();
        }
        if(dissolve.hasDissolved()){
            Die();
        }
    }

    void Heal()
    {
        if(potions > 0)
        {
            currentHealth +=20;
            healthBar.SetHealth(currentHealth);
            potions--;
        }
        potionCount.text = potions.ToString();
    }
    void Die()
    {
        FindObjectOfType<GameManager>().GameOver();
        player.SetActive(false);
        anim.SetBool("Aggro",false);
        ai.enabled = false;
        aid.enabled = false;
        epr.enabled = false;
        camf.enabled = false;
        this.enabled = false;
    }
}
