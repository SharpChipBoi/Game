using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class CharacterStats: MonoBehaviour
{

    public int maxHealth;
    public int currentHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;


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
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }

    public void IncreaseHealth(int level)
    {
        maxHealth += (int)((currentHealth * 0.01f) * ((100 - level) * 0.1f));
        currentHealth = maxHealth;
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }
    public void IncreaseDamage(int level)
    {
        damage.baseVal += (int)((100 - level) * 0.025f);
    }


}
