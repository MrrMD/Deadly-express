using System.Diagnostics;
using Unity.Mathematics;


public static class InventoryAddItem
{
    public static void AddItem(Inventory inventory, Item addedItem)
    {
        foreach (var i in inventory.inventory)
        {
            while (addedItem.Count > 0 && i.ItemName == addedItem.ItemData.ItemName && i.CanStack(1))
            {
                i.IncreaseStack(1);
                addedItem.DecreaseStack(1);
                inventory.TargetRpcInventoryItemCountChange(i.Count, inventory.inventory.IndexOf(i));
                break;
            }
            if (addedItem.Count == 0)
            {
                break;
            }
        }

        foreach (var i in inventory.inventory)
        {
            if (i.ItemName == "Item" && addedItem.Count != 0)
            {
                inventory.inventory[inventory.inventory.IndexOf(i)] = new InventoryItem(addedItem.ItemData, addedItem.ItemData.ItemName, addedItem.Count);
                addedItem.DecreaseStack(addedItem.Count);
                return;
            }
            else if (addedItem.Count != 0)
            {
            }
        }
    }

    public static void AddItemFromChest(Inventory inventory, int itemIndex, int count)
    {
        Chest chest = Utils.InventoryUtils.FindNearbyChest(inventory.transform);

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);
        if (item == null) return;
        int remainingCount = item.Count;
        if (count != 0)
        {
            remainingCount = 1;
        }


        foreach (var i in inventory.inventory)
        {
            while (remainingCount > 0 && i.ItemName == item.ItemName && i.CanStack(1))
            {
                i.IncreaseStack(1);
                remainingCount = remainingCount - 1;
                inventory.TargetRpcInventoryItemCountChange(i.Count, inventory.inventory.IndexOf(i));
            }
            if (remainingCount == 0)
            {
                if(count != 0)
                {
                    chest.RpcChestLootCountChange(itemIndex, item.Count - 1);
                    return;
                }
                else if(count == 0)
                {
                    chest.RpcChestLootRemove(itemIndex);
                }
            }
        }

        foreach (var i in inventory.inventory)
        {
            if (i.ItemName == "Item" && remainingCount != 0)
            {
                inventory.inventory[inventory.inventory.IndexOf(i)] = new InventoryItem(item.ItemData, item.ItemData.ItemName, remainingCount);
                remainingCount = 0;
                if(count == 0)
                {
                    chest.RpcChestLootRemove(itemIndex);
                }
                else if(count == 1)
                {
                    chest.RpcChestLootCountChange(itemIndex, item.Count - 1);
                }

            }
        }
    }

    public static void AddItemFromChestForIndex(Inventory inventory, int itemIndex, int slotIndex, int count)
    {
        int remainingCount = 0;

        Chest chest = Utils.InventoryUtils.FindNearbyChest(inventory.transform);

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);

        if (item == null) return;

        remainingCount = item.Count;

        InventoryItem inventoryitem = inventory.inventory[slotIndex];

        if (inventoryitem.ItemName == "Item")
        {
            inventory.inventory[slotIndex] = new InventoryItem(item);
            if (count != 0)
            {
                inventory.inventory[slotIndex].Count = count;
                item.DecreaseStack(count);
            }
            else if(count == 0) 
            {
                item.DecreaseStack(item.Count);
            }

            if(item.Count == 0)
            {
                chest.RpcChestLootRemove(itemIndex);
            }
            inventory.RpcUpdateInventoryUI();
            inventory.RpcInventoryLootUIUpdate();
            chest.RpcChestLootCountChange(itemIndex, item.Count);
            return;
        }

        if (inventoryitem.ItemName == item.ItemName && inventoryitem.HowMuchCanStack() != 0 && remainingCount != 0)
        {
            var canStackCount = inventoryitem.HowMuchCanStack();

            var stackCount = math.min(canStackCount, item.Count);

            if (count != 0 && stackCount != 0)
            {
                stackCount = 1;
            }

            inventoryitem.IncreaseStack(stackCount);
            item.DecreaseStack(stackCount);
            chest.RpcChestLootCountChange(itemIndex, item.Count);
            inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, slotIndex);
            remainingCount = remainingCount - stackCount;
        }
        chest = null;
    }

    public static void AddItemToChestByIndex(Inventory inventory, int itemIndex, int slotIndex, int count)
    {
        int remainingCount = 0;

        Chest chest = Utils.InventoryUtils.FindNearbyChest(inventory.transform);

        if (chest == null) return;

        InventoryItem chestItem = chest.Inventory.inventory[slotIndex];

        if (chestItem == null) return;

        InventoryItem inventoryitem = inventory.inventory[itemIndex];
        remainingCount = inventoryitem.Count;

        if (chestItem.ItemName == "Item")
        {
            chest.Inventory.inventory[slotIndex] = new InventoryItem(inventoryitem);
            if (count != 0)
            {
                chest.Inventory.inventory[slotIndex].Count = count;
                inventoryitem.DecreaseStack(count);
            }
            else if (count == 0)
            {
                inventoryitem.DecreaseStack(inventoryitem.Count);
            }

            chest.RpcChestLootCountChange(slotIndex, chest.Inventory.inventory[slotIndex].Count);
            inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, itemIndex);
            return;
        }

        if (inventoryitem.ItemName == chest.Inventory.inventory[slotIndex].ItemName && chest.Inventory.inventory[slotIndex].HowMuchCanStack() != 0 && remainingCount != 0)
        {
            var canStackCount = chest.Inventory.inventory[slotIndex].HowMuchCanStack();
            var stackCount = math.min(canStackCount, inventoryitem.Count);

            if (count != 0 && stackCount != 0)
            {
                stackCount = 1;
            }

            chest.Inventory.inventory[slotIndex].IncreaseStack(stackCount);
            inventoryitem.DecreaseStack(stackCount);
            chest.RpcChestLootCountChange(slotIndex, chest.Inventory.inventory[slotIndex].Count);
            inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, itemIndex);
            remainingCount = remainingCount - stackCount;
        }
    }

    public static void AddItemToChest(Inventory inventory, int itemIndex, int count)
    {
        int remainingCount = 0;

        Chest chest = Utils.InventoryUtils.FindNearbyChest(inventory.transform);

        if (chest == null) return;

        Inventory chestInventory = chest.Inventory;

        if (chestInventory == null) return;

        InventoryItem inventoryitem = inventory.inventory[itemIndex];    

        remainingCount = inventoryitem.Count;

        if (count != 0) remainingCount = 1; ;   
        
        for (int i = 0; i < chestInventory.inventory.Count; i++)
        {
            if (inventoryitem.ItemName == chestInventory.inventory[i].ItemName && chestInventory.inventory[i].HowMuchCanStack() != 0 && remainingCount != 0)
            {
                var canStackCount = chest.Inventory.inventory[i].HowMuchCanStack();
                var stackCount = math.min(canStackCount, inventoryitem.Count);

                if (count != 0 && stackCount != 0)
                {
                    stackCount = 1;
                }

                chestInventory.inventory[i].IncreaseStack(stackCount);
                inventoryitem.DecreaseStack(stackCount);
                chest.RpcChestLootCountChange(i, chestInventory.inventory[i].Count);
                inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, itemIndex);
                remainingCount = remainingCount - stackCount;

                if (remainingCount == 0) return;
            }
        }

        if(remainingCount == 0) return;

        for (int i = 0; i < chestInventory.inventory.Count; i++)
        {
            if (chestInventory.inventory[i].ItemName == "Item" )
            {
                chestInventory.inventory[i] = new InventoryItem(inventoryitem);

                if (count != 0)
                {
                    chestInventory.inventory[i].Count = count;
                    inventoryitem.DecreaseStack(count);
                    chestInventory.OnInventoryChanged();
                    inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, itemIndex);
                    return;
                }
                else if (count == 0)
                {
                    inventoryitem.DecreaseStack(inventoryitem.Count);
                    inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, itemIndex);
                    chestInventory.OnInventoryChanged();
                    return;
                }
            }
        }
    }
}
