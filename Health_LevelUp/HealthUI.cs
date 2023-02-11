using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    
    public Transform ui;
    public Image healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged; //Запускаем фунции OnHealthChanged если изменилось здоровье
    }


    void OnHealthChanged(int maxHealth, int currentHealth)//изменяем вид прифаба в зависимости от количества здоровья
    {
        if(ui != null)
        {
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject); //если здоровье <= 0, удаляем UI здоровья 
            }

        }
    }

}
