using InventorySystem;
using Unity.Mathematics;
using UnityEngine;
using Utils;

public static class InventoryItemMove 
{
    public static void MoveOneItem(Inventory inventory,int from, int to)
    {
        var itemFrom = inventory.inventory[from];
        var itemTo = inventory.inventory[to];

        if (inventory.inventory[from].Count == 0) return;
        

        if (itemTo.ItemName == "Item")
        {
            itemTo = new InventoryItem(inventory.inventory[from]);
            itemFrom.DecreaseStack(1);
            itemTo.Count = 1;
        }
        else if (itemFrom.ItemName == itemTo.ItemName && itemTo.CanStack(1))
        {
            itemTo.IncreaseStack(1);
            itemFrom.DecreaseStack(1);
        }
        inventory.inventory[to] = itemTo;
        inventory.inventory[from] = itemFrom;
        inventory.UpdateInventoryUI();
    }
    public static void MoveItemStack(Inventory inventory, int from, int to)
    {
        var itemFrom = inventory.inventory[from];
        var itemTo = inventory.inventory[to];

        if (itemTo.ItemName == "Item" || itemTo.ItemName != itemFrom.ItemName
                                      || (itemFrom.ItemName == itemTo.ItemName && !itemTo.CanStack(1)))
        {
            inventory.inventory[from] = itemTo;
            inventory.inventory[to] = itemFrom;
            return;
        }
        
        if (itemFrom.ItemName == itemTo.ItemName && inventory.inventory[to].HowMuchCanStack() > 0)
        {
            var canStackCount = math.min(inventory.inventory[to].HowMuchCanStack(), itemFrom.Count);
            itemTo.IncreaseStack(canStackCount);
            itemFrom.DecreaseStack(canStackCount);
            inventory.inventory[to] = itemTo;
            inventory.inventory[from] = itemFrom;
        }
    }
}
