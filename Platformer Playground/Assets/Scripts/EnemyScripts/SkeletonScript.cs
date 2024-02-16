using System.Collections;
using TMPro;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int expValue = 5;
    [SerializeField] private int healthTick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private float attackCountDown = 0.0f;
    [SerializeField] private GameObject damageText;
    [SerializeField] private bool readyToAttack = true;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private float walkingSpeed = 40f;
    [SerializeField] private bool facingRight = false;
    [SerializeField] private bool canWalk = true;
    [SerializeField] private bool xpGiven = false;
    [SerializeField] private GameObject gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
        player = GameObject.FindWithTag("Player");
        health = 6;
        healthTick = health;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            if (health <= 0)
            {
                if (!xpGiven)
                {
                    player.GetComponent<PlayerScript>().AddExperience(expValue);
                    xpGiven = true;
                }
                animator.SetBool("Dead", true);
            }
            else if (health < healthTick)
            {
                animator.SetTrigger("Hit");
                healthTick = health;
            }
            if (health > 0)
            {
                Turn();
            }
        }
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 2)
        {
            isWalking = false;
            animator.SetBool("IsWalking", isWalking);
            attackCountDown -= Time.deltaTime;
            rb.velocity = Vector3.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            if (attackCountDown < 0 && readyToAttack)
            {
                readyToAttack = false;
                animator.SetTrigger("Attack");
                StartCoroutine(ResetTimer());
            }
        }
        else
        {
            if (canWalk == true)
            {
                isWalking = true;
                animator.SetBool("IsWalking", isWalking);
                attackCountDown = 0.0f;
                rb.bodyType = RigidbodyType2D.Dynamic;
                if (player.transform.position.x < transform.position.x)
                {
                    rb.velocity = new Vector2(-walkingSpeed * Time.deltaTime, rb.velocity.y);
                }
                if (player.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(walkingSpeed * Time.deltaTime, rb.velocity.y);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (attackCountDown < 0)
        {

        }
        else
        {
            health -= damage;
            GameObject damageNumber = Instantiate(damageText, new Vector2(this.transform.position.x, this.transform.position.y + 0.5f), transform.rotation);
            damageNumber.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
        }
    }

    public void DamagePlayer(GameObject player)
    {
        if (player.GetComponent<AttackScript>().IsBlocking())
        {
            StartCoroutine(player.GetComponent<AttackScript>().Block());
        }
        else
        {
            int damage = Random.Range(1, 3);
            player.GetComponent<PlayerScript>().TakeDamage(damage);

        }
    }
    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(1f);
        attackCountDown = Random.Range(2, 4);
        readyToAttack = true;
    }
    private void Turn()
    {
        if (!facingRight && rb.velocity.x > 0 || facingRight && rb.velocity.x < 0f)
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void OnBecameInvisible()
    {
        if (health <= 0)
        {
            GetComponentInParent<SkeletonSpawnerScript>().ResetSpawner();
            Destroy(this.gameObject);
        }
    }
}
