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
	PlayerManager playerManager;
	LevelUp playerLevel;
	CharacterStats stats;
	public Transform ui;
	Transform cam;


	public int enemyLevel;

	public float enemyXp;

	public float XpMultiplier;

	public TextMeshProUGUI text;


	void Start()
	{
		cam = Camera.main.transform;
		playerManager = PlayerManager.instance;
		playerLevel = playerManager.player.GetComponent<LevelUp>();
		stats = GetComponent<CharacterStats>();
		stats.OnHealthReachedZero += Die;
		SetLevel();

		Debug.Log("XP: " + enemyXp);
	}
    private void LateUpdate()
    {
		if(ui != null)
			ui.forward = -cam.forward;
	}

    // When we interact with the enemy: We attack it.
    public override void Interact()
	{
		print("Interact");
		CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
		if(playerCombat != null)
        {
			playerCombat.Attack(stats);
		}
		
	}

	public void SetLevel()
    {
		enemyLevel = Random.Range(1, playerLevel.level + 10);
		//Scale Enemy XP ----- don't use this if you want to set enemy levels manually.
		enemyXp = Mathf.Round(enemyLevel * 6 * XpMultiplier);


		//Set Text Colour to Orange
		if (enemyLevel == playerLevel.level)
		{
			text.text = "<color=orange>Level: " + enemyLevel + "</color> \n XP: " + enemyXp;
		}

		//This if statement is just used to update the Example UI to reflect the
		//multiplier in the PlayerLevel Class.
		if (enemyLevel < playerLevel.level)
		{
			float multiplier = 1 + (playerLevel.level - enemyLevel) * 0.1f;
			//Set Text Colour to green/
			text.text = "<color=green>Level: " + enemyLevel + "</color> \n XP: " + Mathf.Round(enemyXp * multiplier);
			GetComponent<CharacterStats>().IncreaseDamage(stats.damage.GetValue());
			GetComponent<CharacterStats>().IncreaseHealth(enemyLevel);
		}
		text.text = "<color=red>Level: " + enemyLevel + "</color> \n XP: " + enemyXp;
		GetComponent<CharacterStats>().IncreaseDamage(stats.damage.GetValue());
		GetComponent<CharacterStats>().IncreaseHealth(enemyLevel);

	}
	void Die()
	{
		//Debug.Log("waht");
		//Destroy(gameObject);
	}
}
