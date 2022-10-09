using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    private int maxHealth;
    public Animator bloodAnim;
    private Animator anim;
    public Transform target;
    public float speed = 10.0f;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage = 1;
    private bool vurdu = false;
    private bool dur = false;
    [SerializeField] private Vector3 localScale;
    [SerializeField] castle castleScript;
    public enemyHpBar hpBar;
    private BoxCollider2D bc;
    private void Start()
    {
        anim = GetComponent<Animator>();
        maxHealth = health;
        hpBar.setHealth(health, maxHealth);
        bc = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (health > 0 && vurdu == false&& !dur)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);
            if (transform.position.x>=target.position.x)
            {
                transform.localScale = new Vector3(-localScale.x,localScale.y,localScale.z);
            }
            else 
            {
                transform.localScale = localScale;
            }
        }
        if (vurdu==true)
        {
            timeBtwAttack -= Time.deltaTime;
            if (timeBtwAttack<=0)
            {
                vurdu = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag=="Player"&&timeBtwAttack <= 0)
        {
            if (collision.gameObject.GetComponent<Player>().health>0) {
                vurdu = true;
                anim.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Player>().takeDamage(damage);
                }
            }
            else
            {
                dur=true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && timeBtwAttack <= 0)
        {
            if (collision.gameObject.GetComponent<Player>().health > 0)
            {
                vurdu = true;
                anim.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Player>().takeDamage(damage);
                }
            }
            else
            {
                dur = true;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        hpBar.setHealth(health, maxHealth);
        Invoke("playBloodAnim", 0.2f);
        if (health<=0)
        {
            anim.SetTrigger("Die");
            bc.enabled = false;
        }
    }

    private void playBloodAnim()
    {
        bloodAnim.SetTrigger("bleed");
    }
    private void die()
    {
        castleScript.updateEnemyText();
        Destroy(gameObject);
    }
}
