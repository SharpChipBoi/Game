using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class CharacterStats: MonoBehaviour
{

    public Stat maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;


    public event System.Action OnHealthReachedZero;

    void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }

    public void Damage(int damage)
    {
        damage -= armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }

}
