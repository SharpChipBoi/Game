using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class CharacterStats: MonoBehaviour
{

    public int maxHealth;
    public int currentHealth { get; private set; }//создаем приватный сеттер максимального и нынешнего здоровья

    public Stat damage;//создаем статы
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;//если здоровье измениться можно будет воспользоваться этим действием 


    public event System.Action OnHealthReachedZero;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage) //если нанесли урон
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
    public void Heal(int healAmount)//если поличили 
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die()//если игрок или враг умер
    {
        Debug.Log(transform.name + " died");
    }

    public void IncreaseHealth(int level)//поднять здоровье засчет уровня
    {
        maxHealth += (int)((currentHealth * 0.01f) * ((100 - level) * 0.1f));
        currentHealth = maxHealth;
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }
    public void IncreaseDamage(int level)//поднять урон засчет уровня
    {
        damage.baseVal += (int)((100 - level) * 0.025f);
    }


}
