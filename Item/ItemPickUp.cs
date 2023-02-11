using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemInteract item;
    public GameObject interactButton;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }
    private void Start()
    {
        GameObject playerTransform = GameObject.Find(("ThirdPersonPlayer"));
        player = playerTransform.transform;
        Transform btnGO = player.GetChild(0);
        interactButton = btnGO.gameObject;
    }

    public void PickUp()//когда мы подняли предмет, его добавляем в инвентарь
    {
        bool picked = InventoryPl.instance.AddItem(item);
        if (picked)
        {
            interactButton.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) // проверка на коллизию с игроком, если есть, то включаем UI кнопки
    {
        if (other.tag == "Player")
            interactButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            interactButton.SetActive(false);
    }
}
