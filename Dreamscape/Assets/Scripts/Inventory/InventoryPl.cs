using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPl: MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public static InventoryPl instance;

    public int space = 12;

    public List<ItemInteract> items;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Aaa");
            return;
        }
        instance = this;
    }
    public bool AddItem(ItemInteract item)
    {
        if(items.Count >= space)
        {
            Debug.Log("not enough room");
            return false;
        }
        items.Add(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
        return true;
    }
    public void Remove(ItemInteract item)
    {
        items.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();

    }

    public List<ItemInteract> GetItemList()
    {
        return items;
    }

}

