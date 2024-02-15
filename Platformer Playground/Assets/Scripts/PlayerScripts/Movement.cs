using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpSpeed = 6f;
    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private float groundedVelocity = -1f;
    public UnityEvent OnLandEvent;

    //[SerializeField] private bool isFalling = false;

    [SerializeField] private bool facingRight = true;

    [SerializeField] private GameObject gameController;


    void Start()
    {
        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {

            if(rb.velocity.y < 0 && !IsGrounded())
            {
                //isFalling = true;
                animator.SetBool("IsFalling", true);
            } else { //isFalling = false; 
                animator.SetBool("IsFalling", false);
            }


            animator.SetFloat("Speed", Mathf.Abs(horizontal));
            horizontal = Input.GetAxis("Horizontal");
            if (horizontal == 0 && rb.velocity.y < 0.1f && IsGrounded())
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            } else { rb.constraints = RigidbodyConstraints2D.FreezeRotation; }
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.SetBool("IsJumping", true);
            } else if (IsGrounded() && !animator.GetBool("IsJumping"))
            {
                rb.velocity = new Vector2(rb.velocity.x, groundedVelocity);
            }
            if (!IsGrounded())
            {
                isGrounded = false;
            }
            if (isGrounded == false && IsGrounded() == true)
            {
                isGrounded = true;
                OnLandEvent.Invoke();
            }
            Turn();
        }
    }
    private void FixedUpdate()
    {
        if (IsGrounded() && !GetComponentInChildren<AttackScript>().IsBlocking())
        {
            rb.velocity = new Vector2(horizontal * speed + Time.deltaTime, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundLayer);
    }
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    private void Turn()
    {
        if (facingRight && horizontal > 0f || !facingRight && horizontal < 0f) 
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
