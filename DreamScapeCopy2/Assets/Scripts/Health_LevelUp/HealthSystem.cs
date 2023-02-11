using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Этот скрипт не используется

public class HealthSystem //класс системы здоровья
{
    public static HealthSystem instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    public event EventHandler OnHealthChanged;

    private int health;
    private int healthMax;
    public bool isGameOver = false;

    public HealthSystem(int healthMax) // создаем максимальное здоровье
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()//функция возвращает здоровье
    {
        return health;
    }

    public float GetHealthPercent()//возвращает процент здоровья
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
            isGameOver = true;
        }
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }
    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

}
