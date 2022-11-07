using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5;

    float lastMadeVisible;
    Transform ui;
    Image healthSlider;
    Transform cam;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main.transform;
        foreach (GameObject c in GameObject.FindGameObjectsWithTag("HealthUi"))// находим объект с тэгом HealthUi чтобы подключить к таргету прифаб здоровья
        {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }


    void OnHealthChanged(int maxHealth, int currentHealth)//изменяем вид прифаба в зависимости от количества здоровья
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            lastMadeVisible = Time.time;
            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if (currentHealth <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(ui != null)//если прошло несколько секунд с  момента изменения убираем ЮИ
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
            if(Time.time - lastMadeVisible > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }
}
