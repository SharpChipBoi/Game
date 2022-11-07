using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPl: MonoBehaviour
{

    #region Singleton

    public static InventoryPl instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 12;

    public List<ItemInteract> items;

    public bool AddItem(ItemInteract item)//добавляем предмет в инвентрь
    {
        // ничего не делать если предмет из дефолтных
        if (!item.isDefaultItem)
        {
            //проверить есть ли место
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);    // добавить предмет а лист

            // Триггерим обратный вызов
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }
    public void Remove(ItemInteract item)//удаляем предмет из листа
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }

    public List<ItemInteract> GetItemList()
    {
        return items;
    }

}

