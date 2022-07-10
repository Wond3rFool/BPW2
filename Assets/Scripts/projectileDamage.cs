using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileDamage : MonoBehaviour
{
    float damageAmount = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy;
        if (collision.gameObject.GetComponent<EnemyController>())
        {
            print("Hit enemy");
            enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damageAmount);
        }

    }
}
