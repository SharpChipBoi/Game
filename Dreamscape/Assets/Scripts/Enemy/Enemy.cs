using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
	PlayerManager playerManager;
	CharacterStats stats;

	void Start()
	{
		playerManager = PlayerManager.instance;
		stats = GetComponent<CharacterStats>();
		stats.OnHealthReachedZero += Die;
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

	void Die()
	{
		//Debug.Log("waht");
		//Destroy(gameObject);
	}
}
