using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public float value;

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void HealAmount(float amount) 
    {
        health += amount;
        if (health >= 100) health = 100;
    }
}
