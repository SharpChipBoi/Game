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

    public void AddItem(ItemInteract newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        dropText.faceColor = new Color32(0, 0, 0, 255);
        dropButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        dropButton.interactable = false;
    }

    public void OnDropButton()
    {
        InventoryPl.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

}
