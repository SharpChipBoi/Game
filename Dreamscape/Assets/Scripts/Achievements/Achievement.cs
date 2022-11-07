using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{

    private string name;

    public string Name //позволяет получить доступ из другого скрипта
    {
        get { return name; }
        set { name = value; }
    }

    private string description;

    public string Description 
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int spriteIndex;

    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }


    private GameObject achievementRef;

    private List<Achievement> dependencies = new List<Achievement>();

    private string child;

    public string Child
    {
        get { return child; }
        set { child = value; }
    }

    public Achievement(string name, string description, int spriteIndex, GameObject achievementRef)//Данные об ачивке
    {
        this.name = name; // берем основное название и приравниваем переданному
        this.description = description;
        this.unlocked = false;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        LoadAchievement();
    }


    public void AddDependency(Achievement dependency)
    {
        dependencies.Add(dependency);
    }

    public bool EarnAchievement() //проверка на получение ачивки
    {
        if (!unlocked && !dependencies.Exists(x => x.unlocked == false)) //проверка если хотя бы одна ачивка не открыта, то false
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;

            SaveAchievement(true);

            if (child != null)
            {
                AchievementManager.Instance.EarnAchievement(child); // проверка есть ли листья (children) у ачивок
            }

            return true;
        }
        return false;
    }

    public void SaveAchievement(bool value) // сохраняем данные об открытых ачивках
    {
        unlocked = value;

        PlayerPrefs.SetInt(name, value ? 1 : 0); // проверка на полученную ачивку (если загрузим 1, то открыли ачивку, если 0 то нет)

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;// проверка на полученную ачивку (если загрузим 1, то открыли ачивку, если 0 то нет)
        if (unlocked)
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
        }
    }

}
