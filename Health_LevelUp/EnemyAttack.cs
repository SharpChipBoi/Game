using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    CharacterCombat combat;
    bool nextToEnemy;
    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        CharacterStats targetStats = player.GetComponent<CharacterStats>();
        if (targetStats != null && nextToEnemy)
        {
            combat.Attack(targetStats);//когда враг ряжом с персонажем, враг атакует
        }
    }

    private void OnTriggerEnter(Collider other) //проверка если персонаж рядом с врагом, если да то меняем nextToEnemy на true
    {
        if(other.tag == "Player")
        {
            nextToEnemy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nextToEnemy = false;
        }
    }
}
