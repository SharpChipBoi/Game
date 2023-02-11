using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
	public static Enemy instance;

	public static Enemy Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<Enemy>(); //находим скрипт enemy, кладем его в instance чтобы мы могли использовать его методы
			}
			return Enemy.instance;
		}
	}
	//public PlayerManager playerManager;
	LevelUp playerLevel;
	CharacterStats stats;
	public Transform ui;
	public Transform cam;


	public int enemyLevel;

	public float enemyXp;

	public float XpMultiplier;

	public TextMeshProUGUI text;


	void Start()
	{
		//cam = Camera.main.transform;
		playerLevel = player.GetComponent<LevelUp>();
		stats = GetComponent<CharacterStats>();
		//stats.OnHealthReachedZero += Die;
		SetLevel();

		Debug.Log("XP: " + enemyXp);
	}
	private void LateUpdate()
	{
		if (ui != null)
			ui.forward = -cam.forward;
	}

	// Когда персонаж взаимодействует с врагом он его атакует
	public override void Interact()
	{
		print("Interact");
		CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
		if (playerCombat != null)
		{
			playerCombat.Attack(stats);
		}

	}

	public void SetLevel() //функция, которая задает уровень, опыт, здоровье и урон врагу
	{
		enemyLevel = Random.Range(1, playerLevel.level + 10);
		//получаем информацию о опыте врага, берем уровен рандомно с ограничением по уровню персонажа + 10
		enemyXp = Mathf.Round(enemyLevel * 6 * XpMultiplier);

		stats.damage.baseVal = (int)Mathf.Round(enemyLevel + stats.damage.baseVal);//меняем урон

		//красим в оранжевый если одинаковые уровни
		if (enemyLevel == playerLevel.level)
		{
			text.text = "<color=orange>Level: " + enemyLevel + "</color> \n XP: " + enemyXp;
		}


		if (enemyLevel < playerLevel.level) // если меньше то в зеленый
		{
			float multiplier = 1 + (playerLevel.level - enemyLevel) * 0.1f;
			//красим в зеленый
			text.text = "<color=green>Level: " + enemyLevel + "</color> \n XP: " + Mathf.Round(enemyXp * multiplier);
			GetComponent<CharacterStats>().IncreaseDamage(stats.damage.GetValue());
			GetComponent<CharacterStats>().IncreaseHealth(enemyLevel);
		}
		//красим в красный если больше
		if(enemyLevel > playerLevel.level)
        {
			text.text = "<color=red>Level: " + enemyLevel + "</color> \n XP: " + enemyXp;
			GetComponent<CharacterStats>().IncreaseDamage(stats.damage.GetValue());
			GetComponent<CharacterStats>().IncreaseHealth(enemyLevel);
		}

	}
	void Die()
	{
		//Destroy(gameObject);
	}
}
