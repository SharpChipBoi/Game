﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	public float attackRate = 1f;
	private float attackCountdown = 0f;

	public event System.Action OnAttack;

	//public Transform healthBarPos;

	CharacterStats myStats;
	CharacterStats enemyStats;


	void Start()
	{
		myStats = GetComponent<CharacterStats>();
		//HealthUIManager.instance.Create(healthBarPos, myStats);
	}

	void Update()
	{
		attackCountdown -= Time.deltaTime;
	}

	public void Attack(CharacterStats enemyStats)//атакуем врага(игрок аиакует) или игрока(враг атакует)
	{
		if (attackCountdown <= 0f)
		{
			this.enemyStats = enemyStats;
			attackCountdown = 1f / attackRate;

			StartCoroutine(DoDamage(enemyStats, .6f));

			if (OnAttack != null)
			{
				OnAttack();
			}
		}
	}


	IEnumerator DoDamage(CharacterStats stats, float delay)//кулдаун на атаку
	{
		print("Start");
		yield return new WaitForSeconds(delay);

		Debug.Log(transform.name + " swings for " + myStats.damage.GetValue() + " damage");
		enemyStats.Damage(myStats.damage.GetValue());
	}

}
