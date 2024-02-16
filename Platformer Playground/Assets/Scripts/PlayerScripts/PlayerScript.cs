using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private int expToLevel = 25;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] GameObject deathScreen;
    [SerializeField] private GameObject gameController;
    [SerializeField] private bool invuln = false;
    [SerializeField] private GameObject damageText;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.GetComponent<GameController>().GetPaused())
        {
            if (experience >= expToLevel)
            {
                level++;
                experience = experience - expToLevel;
                expToLevel = (int)Mathf.Floor(0.5f * (level * level) + 25);
            }
            if (health <= 0)
            {
                animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                gameController.GetComponent<GameController>().GameEnd();
                rb.bodyType = RigidbodyType2D.Kinematic;
                GetComponent<SpriteRenderer>().sortingLayerName = "Front";
                deathScreen.SetActive(true);
                animator.SetBool("IsDead", true);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (!invuln)
        {
            health -= damage;
            animator.SetTrigger("IsHit");
            GameObject damageNumber = Instantiate(damageText, new Vector2(transform.position.x, transform.position.y + 1.205f), transform.rotation);
            damageNumber.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
            StartCoroutine(Invulnerable());
        }
    }
    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetExperience()
    {
        return experience;
    }
    public int GetExpToLevel()
    {
        return expToLevel;
    }
    public void AddExperience(int expToAdd)
    {
        experience += expToAdd;
    }

    private IEnumerator Invulnerable()
    {
        invuln = true;
        yield return StartCoroutine(Blink());
        invuln = false;
    }

    private IEnumerator Blink()
    {
        float startTime = Time.deltaTime;
        for (int i = 6; i > 0; i--) 
        { 
            sr.enabled = false;
            yield return new WaitForSeconds(.125f);
            sr.enabled = true;
            yield return new WaitForSeconds(.125f);
        }
        Debug.Log(Time.deltaTime - startTime);
    }
}
