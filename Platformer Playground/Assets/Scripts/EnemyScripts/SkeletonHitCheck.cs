using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHitCheck : MonoBehaviour
{
    [SerializeField] private Collider2D thisCol;
    // Start is called before the first frame update
    void Start()
    {
        thisCol = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<SkeletonScript>().DamagePlayer(col.gameObject);
        }
    }
}
