using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCheckScript : MonoBehaviour
{
    [SerializeField] Collider2D thisCol;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponentInParent<AttackScript>().DamageEnemy(col.gameObject);
        }
    }
}
