using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : ItemInteract
{
    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    //public EquipmentMeshRegion[] coveredMeshRegions; // blend shapes

    public int armorModifier;
    public int damageModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

}

public enum EquipmentSlot { Body, Head, Weapon }
//public enum EquipmentMeshRegion { Body, Head };
