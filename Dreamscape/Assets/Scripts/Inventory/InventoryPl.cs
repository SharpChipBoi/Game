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

    public bool AddItem(ItemInteract item)
    {
        // Don't do anything if it's a default item
        if (!item.isDefaultItem)
        {
            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);    // Add item to list

            // Trigger callback
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }
    public void Remove(ItemInteract item)
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

