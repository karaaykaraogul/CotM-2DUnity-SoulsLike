using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 1f;
    public int attackDamage = 40;
    public float attackRate = 1f;
    public float nextAttackTime = 0f;
    public float attackRange = 0.4f;
    public float aggro = 2f;
    
    private float distance;
    private GameObject player;
    private Transform attackPoint;
    private LayerMask playerLayers;
    private Vector2 currentPos;
    private Vector2 lastPos;
    private GameObject enemy;
    private Vector2 target;

    void Start()
    {
        enemy = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLayers = LayerMask.GetMask("Player");

        target = currentPos + GetRandomVec();

        currentPos = enemy.GetComponent<Transform>().position;
        lastPos = currentPos;
        attackPoint = enemy.transform.GetChild(0);
        distance = Vector2.Distance(currentPos, player.GetComponent<Transform>().position);
    }

    void Update()
    {
        currentPos = enemy.GetComponent<Transform>().position;
        var velocity = (currentPos - lastPos) / Time.deltaTime;
        distance = Vector2.Distance(currentPos, player.GetComponent<Transform>().position);
        
        if(distance > aggro)
        {    
            enemy.GetComponent<Animator>().SetBool("Aggro",false);
            if(Mathf.Abs(currentPos.x - target.x) >= 0.2f)
            {
                enemy.GetComponent<Animator>().SetBool("Aggro",true);
                enemy.transform.position = Vector2.MoveTowards(currentPos,target, speed * Time.deltaTime);
            }
            else
            {
                target = currentPos + GetRandomVec();
            }
        }
        else
        {
            enemy.GetComponent<Animator>().SetBool("Aggro",true);
            if(enemy.CompareTag("Golem"))
            {
                if(distance > 0.7f)
                {
                    enemy.transform.position = Vector2.MoveTowards(currentPos,player.transform.position, speed * Time.deltaTime);
                }
            }
            else
            {
                if(distance > 0.29f)
                {
                    enemy.transform.position = Vector2.MoveTowards(currentPos,player.GetComponent<Transform>().position, speed * Time.deltaTime);
                }
            }

            if (Time.time >= nextAttackTime)
            {
                if (Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers).Length != 0)
                {  
                    if(enemy.CompareTag("Golem"))
                    {
                        enemy.GetComponent<Animator>().SetTrigger("IsAttacking");
                    } 
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        if(velocity.x >= 0.05f)
        {    
            enemy.transform.localScale = new Vector3(-1 ,enemy.transform.localScale.y,1);
        }
        else if (velocity.x < -0.05f)
        {
            enemy.transform.localScale = new Vector3(1 ,enemy.transform.localScale.y,1);
        }

        lastPos = currentPos;
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHP>().TakeDamage(attackDamage);
        }
    }
    
    public static Vector2 GetRandomVec()
    {
        return new Vector2(Random.Range(-1f,1f),0).normalized;
    }
}
