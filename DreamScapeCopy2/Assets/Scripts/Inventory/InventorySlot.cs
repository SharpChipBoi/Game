using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button dropButton;
    ItemInteract item;
    public TextMeshProUGUI dropText;
    public Transform playerPosition;

    private void Start()
    {
        GameObject playerTransform = GameObject.Find(("ThirdPersonPlayer"));
        playerPosition = playerTransform.transform;
        playerPosition = playerPosition.GetChild(1);
    }

    public void AddItem(ItemInteract newItem) // добавляем предмет (активируем кнопку выброса и активировуем иконку)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        dropText.faceColor = new Color32(0, 0, 0, 255);
        dropButton.interactable = true;
    }

    public void ClearSlot() // слот принемает вид пустого слота (неактивна кнопка выброса и деактивирована иконка)
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        dropButton.interactable = false;
    }

    public void OnDropButton() // удаляем из слота предмет и создаем его на сцене
    {
        Instantiate(item.itemPrefab, playerPosition.transform.position, playerPosition.transform.rotation);
        InventoryPl.instance.Remove(item);
    }

    public void UseItem() // изпользовать предмет
    {
        if (item != null)
        {
            item.Use();
        }
    }

}
