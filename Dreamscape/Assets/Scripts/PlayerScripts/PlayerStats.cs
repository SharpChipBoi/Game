using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;//если у нас изменились предметы
    }

    // Update is called once per frame
    void onEquipmentChanged(Equipment newItem, Equipment oldItem) //если есть новые предметы, которые мы экипировали у которых есть усилители проверяем 
    {
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }

    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }

}
