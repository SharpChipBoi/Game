using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class CharacterStats: MonoBehaviour
{
    public static CharacterStats instance;

    public AudioSource hurt;

    public int maxHealth;
    public int currentHealth { get; private set; }//создаем приватный сеттер максимального и нынешнего здоровья

    public Stat damage;//создаем статы
    public Stat armor;

    public event System.Action<int, int> OnHealthChanged;//если здоровье измениться можно будет воспользоваться этим действием 


    public event System.Action OnHealthReachedZero;

    void Awake()
    {
        currentHealth = maxHealth;
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    public void Damage(int damage) //если нанесли урон меняем значение здоровья и при здоровье = 0 убиваем персонажа
    {
        damage -= armor.GetValue();

        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        hurt.Play();
        if(OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die_player();
        }
    }
    public void Heal(int healAmount)//если поличили  то добавляем здоровье
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    public virtual void Die_player()//Функция для проверки если игрок или враг умер 
    {
        Debug.Log(transform.name + " died");
    }

    public void IncreaseHealth(int level)//При подняии здоровье засчет уровня меняем интерфейс
    {
        maxHealth += (int)((currentHealth * 0.01f) * ((100 - level) * 0.1f));
        currentHealth = maxHealth;
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }
    }
    public void IncreaseDamage(int level)//поднимает урон засчет уровня персонажу
    {
        damage.baseVal += (int)((100 - level) * 0.025f);
    }


}
