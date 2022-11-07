using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{

    private string name;

    private string description;

    private bool unlocked;

    private int spriteIndex;

    private GameObject achievementRef;

    public Achievement(string name, string description, int spriteIndex, GameObject achievementRef)
    {
        this.name = name; // берем верхний нейм и приравниваем переданному
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
    }


    public bool EarnAchievement() //проверка на получение ачивки
    {
        if (!unlocked)
        {
            unlocked = true;
            return true;
        }
        return false;
    }

}
