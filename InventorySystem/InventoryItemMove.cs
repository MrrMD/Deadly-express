public static class InventoryItemMove 
{
    public static void MoveOneItem(Inventory inventory,int from, int to)
    {
        if (inventory.inventory[from].Count == 0)
        {
            return;
        }

        if (inventory.inventory[to].ItemName == "Item")
        {
            InventoryItem item = new InventoryItem(inventory.inventory[from]);
            inventory.inventory[from].DecreaseStack(1);
            inventory.TargetRpcInventoryItemCountChange(inventory.inventory[from].Count, from);
            item.Count = 1;
            inventory.inventory[to] = item;
        }
        else if (inventory.inventory[from].ItemName == inventory.inventory[to].ItemName && inventory.inventory[to].CanStack(1))
        {
            inventory.inventory[to].IncreaseStack(1);
            inventory.inventory[from].DecreaseStack(1);
            inventory.TargetRpcInventoryItemCountChange(inventory.inventory[to].Count, to);
            inventory.TargetRpcInventoryItemCountChange(inventory.inventory[from].Count, from);
        }
        else
        {
            inventory.UpdateInventoryUI();
        }
    }
    public static void MoveItemStack(Inventory inventory, int from, int to)
    {

        if (inventory.inventory[to].ItemName == "Item" || inventory.inventory[to].ItemName != inventory.inventory[from].ItemName
            || (inventory.inventory[from].ItemName == inventory.inventory[to].ItemName && !inventory.inventory[to].CanStack(1)))
        {
            InventoryItem item = inventory.inventory[from];
            inventory.inventory[from] = inventory.inventory[to];
            inventory.inventory[to] = item;
            return;
        }

        var firstitemCount = inventory.inventory[from].Count;

        for (int i = 0; i < firstitemCount; i++)
        {
            if (inventory.inventory[from].ItemName == inventory.inventory[to].ItemName && inventory.inventory[to].CanStack(1))
            {
                inventory.inventory[to].IncreaseStack(1);
                inventory.inventory[from].DecreaseStack(1);
                inventory.TargetRpcInventoryItemCountChange(inventory.inventory[to].Count, to);
                inventory.TargetRpcInventoryItemCountChange(inventory.inventory[from].Count, from);
            }
        }
    }
}
