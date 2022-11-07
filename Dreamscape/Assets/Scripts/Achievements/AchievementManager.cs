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

    // Start is called before the first frame update
    void Start()
    {

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();
        CreateAchievement("General", "Press W", "Press W to unlock this achievement", 0);

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
    }

    public void EarnAchievement(string title) //Получаем ачивку
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            StartCoroutine(HideAchievement(achievement)); 
        }
    }

    public IEnumerator HideAchievement(GameObject achievement) //Показываемна экране 4 секунды потом удаляем
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string category, string title, string description, int spriteIndex)//создаем ачивку
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achievement newAchievement = new Achievement(name, description, spriteIndex, achievement);

        achievements.Add(title, newAchievement);

        
        SetAchievementInfo(category, achievement, title, description, spriteIndex);
    }

    public void SetAchievementInfo(string category, GameObject achievement, string title, string description, int spriteIndex)//выдает всю информацию об ачивке (размер,какое изображение использовать, чей ребонок и тд.)
    {
        achievement.transform.SetParent(GameObject.Find(category).transform);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title;
        achievement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[spriteIndex];
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
