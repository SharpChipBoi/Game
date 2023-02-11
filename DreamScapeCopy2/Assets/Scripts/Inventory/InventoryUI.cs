using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    InventoryPl inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryPl.instance;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventory.onItemChangedCallback += UpdateUI;//обнавляем ЮИ в зависемости от изменений
        //Debug.Log(slots.Length);
        UpdateUI();
        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i] == null)
            {
                Debug.Log(slots[i]);
                slots[i].icon.enabled = false;
                slots[i].dropButton.interactable = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);//включаем ЮИ при нажатии кнопки инвентаря (i)
        }
    }
    void UpdateUI()//обновляем интрерфейс учитывая добавленные объекты, если их нет, то очищаем слот 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
