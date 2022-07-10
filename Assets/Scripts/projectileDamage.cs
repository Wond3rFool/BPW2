using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    float damageAmount = 100f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy;
        if (collision.gameObject.GetComponentInChildren<EnemyController>())
        {
            print("Hit enemy");
            enemy = collision.gameObject.GetComponentInChildren<EnemyController>();
            enemy.TakeDamage(damageAmount);
        }

    }
}
