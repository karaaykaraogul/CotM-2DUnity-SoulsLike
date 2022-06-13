using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GolemAttack : MonoBehaviour
{
    public AIPath aiPath;
    public Transform attackPoint;
    public Transform player;
    public Transform golem;
    public Animator anim;
    public float attackRange = 0.4f;
    public LayerMask playerLayers;
    public int attackDamage = 40;
    public float attackRate = 1f;
    float nextAttackTime = 0f;
    public float aggro = 2f;
    float distance;
    void Start()
    {
        distance = Vector2.Distance(golem.position, player.position);
    }
    void Update()
    {
        distance = Vector2.Distance(golem.position, player.position);
        if(distance>aggro)
        {
            anim.SetBool("Aggro",false);
            aiPath.enabled=false;
        }
        else{
            anim.SetBool("Aggro",true);
            aiPath.enabled=true;
        }
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] check = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
            if (check.Length > 0)
            {
                anim.SetTrigger("IsAttacking");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }

    void Attack()
    {

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHP>().TakeDamage(attackDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
