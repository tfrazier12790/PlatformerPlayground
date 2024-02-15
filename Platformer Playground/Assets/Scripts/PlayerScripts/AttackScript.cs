using System.Collections;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private int attackNumber = 0;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float timeToAttack = 0.6f;
    [SerializeField] private GameObject damageText;
    [SerializeField] private bool isBlocking = false;
    [SerializeField] private bool canBlock = true;
    [SerializeField] private GameObject gameController;

    private void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
    }
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            if (Input.GetButton("Horizontal")) { canAttack = false; }
            if (Input.GetButtonUp("Horizontal")) { canAttack = true; }

            timeToAttack -= Time.deltaTime;
            if (Input.GetButtonDown("Fire1"))
            {
                if (canAttack && !isBlocking)
                {
                    //canAttack = false;
                    //StartCoroutine(Attack());
                    attackNumber = attackNumber + 1;
                }
            }
            if (Input.GetButton("Fire2"))
            {
                if (canBlock && GetComponent<Movement>().IsGrounded())
                {
                    animator.SetBool("IsBlocking", true);
                    isBlocking = true;
                }
            }
            if (Input.GetButtonUp("Fire2"))
            {
                animator.SetBool("IsBlocking", false);
                isBlocking = false;
            }
            if (isBlocking)
            {
                rb.velocity = Vector3.zero;
            }
            if (attackNumber > 3)
            {
                attackNumber = 0;
            }
            if (timeToAttack <= 0)
            {
                timeToAttack = 0;
            }
            animator.SetInteger("AttackNumber", attackNumber);
        }
    }

    IEnumerator Attack()
    {
        attackNumber++;

        if (attackNumber > 3) { attackNumber = 1; }
        if (attackNumber == 1)
        {
            timeToAttack = 0.6f;
            yield return new WaitForSeconds(0.25f);
        }
        else
        {
            timeToAttack = 0.7f;
            yield return new WaitForSeconds(0.35f);
        }
        canAttack = true;
    }
    public void ResetAttackNum()
    {
        attackNumber = 0;
    }

    public void DamageEnemy(GameObject enemy)
    {
        int damage = attackNumber;//Random.Range(1, 10);
        enemy.SendMessageUpwards("TakeDamage", damage);
    }
    public IEnumerator Block()
    {
        animator.SetTrigger("Blocked");
        canAttack = false;
        yield return new WaitForSeconds(0.6f);
        canAttack = true;
    }
    public bool IsBlocking()
    {
        return isBlocking;
    }
}
