using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : Interactable
{
    public int healAmount;

    PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;
    }


    public override void Interact()
    {
        base.Interact();
        Debug.Log("healed: " + 20);
        playerManager.player.GetComponent<CharacterStats>().Heal(healAmount);
    }

}
