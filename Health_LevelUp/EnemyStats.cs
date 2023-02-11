using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Transform player;

    void Start()
    {
        player = GameObject.Find("ThirdPersonPlayer").transform;
    }

    public override void Die_player() //функция, которая удаляет объект (враг) и возвращает игроку опыт
    {

        base.Die_player();

        player.GetComponent<LevelUp>().GainExp(Enemy.Instance.enemyXp);//при смерти врага даем игроку то количество опыта которое есть у врага
        player.GetComponent<LevelUp>().UpdateXpUI();
        Debug.Log("XP gained: " + Enemy.Instance.enemyXp);
        Destroy(gameObject);
    }
}