using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class CharacterStats: MonoBehaviour
{

    public float xp;

    public float maxHealth;
    public float currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public event System.Action<float, float> OnHealthChanged;


    public event System.Action OnHealthReachedZero;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        damage -= armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }

    public void IncreaseHealth(int level)
    {
        maxHealth += (currentHealth * 0.01f) * ((100 - level) * 0.1f);
        currentHealth = maxHealth;
    }
    public void IncreaseDamage(int level)
    {
        float currentdamage = damage.GetValue(); 
        currentdamage = (currentdamage * 0.01f) * ((100 - level) * 0.1f);
    }


}
