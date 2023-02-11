using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;//если у нас изменились предметы запускаем функцию onEquipmentChanged
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

    public override void Die_player() //вызывем функцию убивающая игрока 
    {
        base.Die_player();
        //Destroy(this.gameObject);
        KillPlayer();
        //Die.instance.
    }
    public void KillPlayer() // когда игрок умирает запускаем главнцю сцену
    {
        SceneManager.LoadScene("Main");
    }
}