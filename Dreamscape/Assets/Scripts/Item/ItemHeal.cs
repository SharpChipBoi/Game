﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : Interactable
{
    public int healAmount;

    public override void Interact()//когдв мы подняли предмет подключаем функцию HEal и лечим  указанное количество здоровья
    {
        base.Interact();
        Debug.Log("healed: " + healAmount);
        PlayerManager.Instance.player.GetComponent<CharacterStats>().Heal(healAmount);
        Destroy(gameObject);
    }

}
