using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	public float attackRate = 1f;
	private float attackCountdown = 0f;

	public event System.Action OnAttack;


	CharacterStats myStats;
	CharacterStats enemyStats;
	public Animator anim;

	void Start()
	{
		myStats = GetComponent<CharacterStats>(); // передаем класс в переменную myStats
	}

	void Update()
	{
		attackCountdown -= Time.deltaTime; // каждый кадр уменьшаем время кулдауна
	}

	public void Attack(CharacterStats enemyStats)//атакуем врага(игрок атакует) или игрока(враг атакует)
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

		anim.SetBool("isAttacking", true);
		print("Start");
		yield return new WaitForSeconds(delay); // ждем определенное время, прежде чем выполнять дальше функцию

		anim.SetBool("isAttacking", false);
		Debug.Log(transform.name + " swings for " + myStats.damage.GetValue() + " damage");
		enemyStats.Damage(myStats.damage.GetValue());
	}

}
