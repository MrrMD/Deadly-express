using InventorySystem;
using Unity.Mathematics;
using UnityEngine;

public static class InventoryItemMove 
{
    public static void MoveOneItem(Inventory inventory,int from, int to)
    {
        var itemFrom = inventory.inventory[from];
        var itemTo = inventory.inventory[to];

        if (inventory.inventory[from].Count == 0)
        {
            return;
        }

        if (inventory.inventory[to].ItemName == "Item")
        {
            itemTo = new InventoryItem(inventory.inventory[from]);
            itemFrom.DecreaseStack(1);
            itemTo.Count = 1;
        }
        else if (inventory.inventory[from].ItemName == inventory.inventory[to].ItemName && inventory.inventory[to].CanStack(1))
        {
            itemTo.IncreaseStack(1);
            itemFrom.DecreaseStack(1);
        }
        inventory.inventory[to] = itemTo;
        inventory.inventory[from] = itemFrom;
    }
    public static void MoveItemStack(Inventory inventory, int from, int to)
    {
        var itemFrom = inventory.inventory[from];
        var itemTo = inventory.inventory[to];

        if (itemTo.ItemName == "Item" || itemTo.ItemName != itemFrom.ItemName
                                      || (itemFrom.ItemName == itemTo.ItemName && !itemTo.CanStack(1)))
        {
            inventory.inventory[from] = itemFrom;
            inventory.inventory[to] = itemTo;
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
