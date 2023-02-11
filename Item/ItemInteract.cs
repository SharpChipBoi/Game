using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemInteract : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public bool isHealing = false;
    public bool isGreyKey = false;
    public bool isGoldKey = false;
    public GameObject itemPrefab;
    public Material itemMaterial;

    public virtual void Use() // используем предмет (функцию можно перепысывать в других классах)
    {
        Debug.Log("Used " + name);
    }

    public void RemoveFromInventory() //удаляем из инвентаря
    {
        InventoryPl.instance.Remove(this);
    }
}
