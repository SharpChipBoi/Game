using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrapAttack : MonoBehaviour
{
    public Transform player;
    bool nextToEnemy;
    public float eatCD;
    public Animator anim;


    // Update is called once per frame
    void Update()
    {
        if (eatCD <= 0 && nextToEnemy) // если игрок рядом то запускаем корутину и атакуем игрока
        {
            StartCoroutine(_EatPlayer());
        }
        else
        {
            StopAllCoroutines();
            eatCD -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other) // проверяем коллизию с игроком
    {
        if (other.tag == "Player")
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
    IEnumerator _EatPlayer()//проверка на кулдаун
    {
        anim.SetBool("BiteTime", true);
        CharacterStats.instance.Damage(20);
        yield return new WaitForSeconds(4f);
        anim.SetBool("BiteTime", false);
    }
}
