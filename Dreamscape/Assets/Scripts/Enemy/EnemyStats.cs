using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    public override void Die()
    {
        
        base.Die();
        playerManager.player.GetComponent<LevelUp>().GainExp(xp);
        Debug.Log("XP gained: " + xp);
        Destroy(gameObject);
    }
}
