using Unity.Mathematics;
using UnityEngine;

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

    public static void addItemFromChest(Inventory inventory, int itemIndex, int slotIndex)
    {
        int remainingCount = 0;

        Chest chest = Utils.InventoryUtils.Find(inventory.transform);

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);
        chest = null;

        if (item == null) return;

        remainingCount = item.Count;

        foreach (var i in inventory.inventory)
        {
            if (i.ItemName == "Item") continue;

            if (i.ItemName == item.ItemName && i.HowMuchCanStack() != 0 && remainingCount != 0)
            {
                var canStackCount = i.HowMuchCanStack();
                var StackCount = math.min(canStackCount, item.Count);
                Debug.Log(canStackCount);

                i.IncreaseStack(StackCount);
                item.DecreaseStack(StackCount);
                inventory.RpcInventoryLootUIUpdate();
                inventory.TargetRpcInventoryItemCountChange(i.Count, inventory.inventory.IndexOf(i));
                remainingCount = remainingCount - StackCount;
            }
        }
        if (remainingCount != 0)
        {
            foreach (var i in inventory.inventory)
            {
                if (i.ItemName == "Item")
                {
                    inventory.inventory[inventory.inventory.IndexOf(i)] = new InventoryItem(item);
                    item.DecreaseStack(item.Count);
                    inventory.RpcInventoryLootUIUpdate();
                    remainingCount = 0;
                    break;
                }
            }
        }
    }

    public static void addItemFromChestForIndex(Inventory inventory, int itemIndex, int slotIndex)
    {
        int remainingCount = 0;

        Chest chest = Utils.InventoryUtils.Find(inventory.transform);

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);
        chest = null;

        if (item == null) return;

        remainingCount = item.Count;

        InventoryItem inventoryitem = inventory.inventory[slotIndex];

        if (inventoryitem.ItemName == "Item")
        {
            inventory.inventory[slotIndex] = new InventoryItem(item);
            item.DecreaseStack(item.Count);
            inventory.RpcInventoryLootUIUpdate();
            remainingCount = 0;
            return;
        }

        if (inventoryitem.ItemName == item.ItemName && inventoryitem.HowMuchCanStack() != 0 && remainingCount != 0)
        {
            var canStackCount = inventoryitem.HowMuchCanStack();
            var StackCount = math.min(canStackCount, item.Count);
            Debug.Log(canStackCount);

            inventoryitem.IncreaseStack(StackCount);
            item.DecreaseStack(StackCount);
            inventory.RpcInventoryLootUIUpdate();
            inventory.TargetRpcInventoryItemCountChange(inventoryitem.Count, slotIndex);
            remainingCount = remainingCount - StackCount;
        }
    }
    
}
