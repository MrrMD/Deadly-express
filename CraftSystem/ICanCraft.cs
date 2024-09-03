using Mirror;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class ICanCraft : NetworkBehaviour, IUsable
{
    [SerializeField] private List<ItemData> craftList;
    [SerializeField] private Inventory Inventory;

    [Command]
    public void Use()
    {
        Debug.Log("Crafter is open");
        //Открыть UI и поднянуть айтемы
    }

    //public Item CraftItem(Item item)
    //{
    //    if (!CanCraft(item)) return null;
    //    return item;
    //}

    //private bool CanCraft(Item item)
    //{
    //    if (item == null) return false;

    //    if (!item.IsCraftable)
    //    {
    //        Debug.Log("This item is not craftable");
    //        return false;
    //    }

    //    if (Inventory.inventory.Contains(item.CraftItems[0]) &&
    //        Inventory.inventory.Contains(item.CraftItems[1]))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("You dont have items for craft it");
    //        return false;
    //    }
    //}

    public List<ItemData> GetCraftItems() 
    {
        return craftList;
    }
}
