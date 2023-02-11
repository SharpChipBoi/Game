using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{
    public GameObject achievementList;

    public Sprite neatural, highlight;

    private Image sprite;

    private void Awake()
    {
        sprite = GetComponent<Image>();
    }


    public void Click() //меняем цвет в зависимости от нажатия кнопки
    {
        if(sprite.sprite == neatural)
        {
            sprite.sprite = highlight;
            achievementList.SetActive(true);
        }
        else
        {
            sprite.sprite = neatural;
            achievementList.SetActive(false);
        }
    }
}
