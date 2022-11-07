using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public GameObject achievementPrefab;

    public Sprite[] sprites;

    private AchievementButton activeButton;

    public ScrollRect scrollRect;

    public GameObject achievementMenu;

    public GameObject visualAchievement;

    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockedSprite;

    private static AchievementManager instance; // создаем сингалтон чтобы получить доступ к классу и к его методам

    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManager>(); //находит ачивмент менеджер и  пихает его в instance чьлбы мы могли использовать его класс
            }
            return AchievementManager.instance; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll(); // очистить дату от сохраненной 
        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();
        CreateAchievement("General", "Press W", "Press W to unlock this achievement", 0);
        CreateAchievement("General", "Press A", "Press A to unlock this achievement", 0);
        CreateAchievement("General", "Press S", "Press S to unlock this achievement", 0);
        CreateAchievement("General", "Press D", "Press D to unlock this achievement", 0);
        CreateAchievement("General", "All keys", "Press all keys to unlock", 1, new string[] { "Press W" ,"Press A", "Press S", "Press D"});


        CreateAchievement("Other", "Dash", "Find out how to dash", 2);
        CreateAchievement("Other", "Jump", "Find out how to jump", 3);
        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList")) //смотрим у кого есть тэг ачивментлист и отключаем, чтобы не было overlapping
        {
            achievementList.SetActive(false);
        }
        

        activeButton.Click();

        achievementMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            achievementMenu.SetActive(!achievementMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("Press W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievement("Press S");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EarnAchievement("Press A");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EarnAchievement("Press D");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EarnAchievement("Dash");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EarnAchievement("Jump");
        }
    }

    public void EarnAchievement(string title) //Получаем ачивку и показываем ее на экране
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            achievement.transform.localScale = new Vector3(5, 1, 1);
            StartCoroutine(HideAchievement(achievement)); 
        }
    }

    public IEnumerator HideAchievement(GameObject achievement) //Показываемна экране 4 секунды потом удаляем
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title, string description, int spriteIndex, string[] dependencies = null)//создаем ачивку. Если мв не хоти создавать ачивку, которая зависит от выполнения других, то просто ничего не пишеи при использовании ф-ции 
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achievement newAchievement = new Achievement(name, description, spriteIndex, achievement);

        achievements.Add(title, newAchievement);

        SetAchievementInfo(parent, achievement, title);

        if (dependencies != null)
        {
            foreach(string achievementTitle in dependencies) //ищем правильное название ачивки и создаем зависимость 
            {
                Achievement dependency = achievements[achievementTitle];
                dependency.Child = title;
                newAchievement.AddDependency(dependency);
            }
            //создали связь между ачивками dependancy = "Press Space" <-- Child = "Press W"
            //NewAchievement = "Press W" --> Parent = "Press Space" 
        }

    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)//выдает всю информацию об ачивке из словаря achievements(размер,какое изображение использовать, чей ребонок и тд.)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }

    public void ChangeCategory(GameObject button) //меняем активированное поле(категорию)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();

        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

        achievementButton.Click();
        activeButton.Click();

        activeButton = achievementButton;


    }

}
