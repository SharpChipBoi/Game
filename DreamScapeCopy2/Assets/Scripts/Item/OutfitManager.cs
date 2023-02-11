using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Outfit", menuName = "Inventory/Outfit")]
public class OutfitManager : ItemInteract
{
    public int ind;
    public Material material;
    public override void Use() //Меняем тектуру персонажа и удаляем предмет из инвентаря
    {
        base.Use();
        ChangeMaterial.instance.ChangeMat(ind, material, this);
        RemoveFromInventory();
    }
}
