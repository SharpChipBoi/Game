using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        RainPacket,
        Sword,
        Clothes,
    }
    public ItemType itemType;
    public int amount;
    
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
            case ItemType.Clothes:      return ItemAssets.Instance.clothesSprite;
            case ItemType.RainPacket:   return ItemAssets.Instance.rainPacketSprite;
        }
    }

}
