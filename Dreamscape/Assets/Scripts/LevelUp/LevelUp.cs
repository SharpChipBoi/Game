using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CharacterStats))]
public class LevelUp : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    float visibleTime = 5;

    float lastMadeVisible;
    Transform ui;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;
    [Header("UI")]
    public Image xpSliderFront;
    public Image xpBack;
    Transform cam;

    public int level;
    private float currentXP = 0;
    private float requiredXP;

    private float lerpTimer;
    private float delayTimer;

    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f,4f)]
    public float powerMultiplier = 2;
    [Range(7f,14f)]
    public float divisionMultiplier = 7;


    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                xpSliderFront = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(true);
                break;
            }
        }
        xpSliderFront.fillAmount = currentXP / requiredXP;
        xpBack.fillAmount = currentXP / requiredXP;
        requiredXP = CalculateRequiredXp();
        levelText.text = "Level" + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
            GainExp(200);
        if (currentXP > requiredXP)
            GainLevel();
    }



    void UpdateXpUI()
    {
        float xpFraction = currentXP / requiredXP;
        float xpFront = xpSliderFront.fillAmount;
        if(xpFront < xpFraction)
        {
            delayTimer += Time.deltaTime;
            xpBack.fillAmount = xpFraction;
            if(delayTimer > 2)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                xpSliderFront.fillAmount = Mathf.Lerp(xpFront, xpBack.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXP + "/" + requiredXP;
    }


    public void GainExp(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void GainXpScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXP += xpGained * multiplier;
        }
        else
        {
            currentXP += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }


    public void GainLevel()
    {
        level++;
        xpSliderFront.fillAmount = 0f;
        xpBack.fillAmount = 0f;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        GetComponent<CharacterStats>().IncreaseHealth(level);
        GetComponent<CharacterStats>().IncreaseDamage(level);
        requiredXP = CalculateRequiredXp();
        levelText.text = "Level: " + level;
    }

    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for(int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }

    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;
            //if (Time.time - lastMadeVisible > visibleTime)
            //{
            //    ui.gameObject.SetActive(false);
            //}
        }
    }

}
