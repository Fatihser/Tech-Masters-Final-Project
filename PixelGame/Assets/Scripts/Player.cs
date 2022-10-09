using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int walkSpeed=5;
    private Animator animController;
    [SerializeField]private Vector3 localScale;
    private Vector3 moveDelta;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage = 10;
    public int health = 100;
    public GameObject gmPanel;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] Image hpImage;
    private void Start()
    {
        animController = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (health>0)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            moveDelta = new Vector3(x, y, 0);

            if (moveDelta.x > 0)
            {
                transform.localScale = localScale;
                animController.SetBool("running", true);
            }
            else if (moveDelta.x < 0)
            {
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
                animController.SetBool("running", true);
            }
            if (moveDelta.y != 0)
            {
                animController.SetBool("running", true);
            }
            else if (moveDelta.y == 0 && moveDelta.x == 0)
            {
                animController.SetBool("running", false);
            }



            transform.Translate(moveDelta * Time.deltaTime * walkSpeed);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && moveDelta.x == 0 && moveDelta.y == 0&&timeBtwAttack<=0 &&health>0)
        {
            animController.SetTrigger("attack");
            timeBtwAttack = startTimeBtwAttack;
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().takeDamage(damage);
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
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
        hpText.text = health.ToString();
        hpImage.fillAmount = (float)health / 100;
        if (health<=0)
        {
            animController.SetTrigger("die");
        }
    }

    private void die()
    {
        //Destroy(gameObject);
        gmPanel.SetActive(true);
    }
}