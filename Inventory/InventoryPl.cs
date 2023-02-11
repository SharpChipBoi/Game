using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class InventoryPl: MonoBehaviour
{


    public static InventoryPl instance;


    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public AudioSource addToInv;
    //public AudioSource removeFromInv;

    public int space = 6;

    public List<ItemInteract> allItems;
    public List<ItemInteract> items;
    public bool dontPlayAudio;

   
    private void Awake()
    {


        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }


        instance = this;
    }
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
            if (!dontPlayAudio)
            {
                addToInv.Play();
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
        //removeFromInv.Play();
        Debug.Log("Dropped");
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }

    public List<ItemInteract> GetItemList() //возвращает список с предметами
    {
        return items;
    }

    public void SaveInventory() //сохраняем все данные об ивентаре (предметы)
    {
        items = GetItemList();

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i].name);
        }
        PlayerPrefs.SetInt("size", items.Count);
        Debug.Log(items.Count);
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                PlayerPrefs.SetString(i.ToString(), items[i].name);
            }
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].RemoveFromInventory();
        }
    }
    public void LoadInventory() //загружаем данные об инветнаря (предметы)
    {
        string tmp;
        dontPlayAudio = true;
        int size = PlayerPrefs.GetInt("size");
        for (int i = 0; i < size; i++)
        {
            tmp = PlayerPrefs.GetString(i.ToString());
            //Debug.Log(tmp);
            if (tmp == "")
            {
                dontPlayAudio = false;
                break;
            }
            for (int j = 0; j < allItems.Count; j++)
            {
                if (allItems[j].name == tmp)
                {
                    AddItem(allItems[j]);
                }

            }
        }
    }
    [Serializable]
    private class SaveObject
    {
        public int itemsAmount;
        public List<ItemInteract> itemsSaved;
    }

}

