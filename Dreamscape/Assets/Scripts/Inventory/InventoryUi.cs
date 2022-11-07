using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    InventorySlot[] slots;

    InventoryPl inventory; 
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryPl.instance;
        inventory.onItemChangedCallback += UpdateUI;//обнавляем ЮИ в зависемости от изменений

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);//включаем ЮИ при нажатии кнопки инвентаря(i)
        }
    }
    void UpdateUI()//обновляем интрерфейс
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else{
                slots[i].ClearSlot();
            }
        }
    }
}
