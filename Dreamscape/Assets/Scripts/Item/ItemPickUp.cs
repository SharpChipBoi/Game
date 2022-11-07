using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public ItemInteract item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()//когда мы подняли предмет, его добавляем в инвентарь
    {        
        bool picked = InventoryPl.instance.AddItem(item);
        if(picked)
            Destroy(gameObject);
    }


}
