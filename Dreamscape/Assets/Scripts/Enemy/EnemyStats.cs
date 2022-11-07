using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    public override void Die()
    {
        
        base.Die();

        PlayerManager.Instance.player.GetComponent<LevelUp>().GainExp(Enemy.Instance.enemyXp);//при смерти врага даем игроку то количество опыта которое есть у врага
        PlayerManager.Instance.player.GetComponent<LevelUp>().UpdateXpUI();
        Debug.Log("XP gained: " + Enemy.Instance.enemyXp);
        Destroy(gameObject);
    }
}
