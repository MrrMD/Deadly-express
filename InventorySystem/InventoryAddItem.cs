using System;
using Unity.Mathematics;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryAddItem
    {
        public static void AddItem(Inventory inventory, Item addedItem)
        {
            for (int index = 0; index < inventory.inventory.Count; index++)
            {
                InventoryItem i = inventory.inventory[index];
                if (addedItem.Count > 0 && i.ItemName == addedItem.ItemData.ItemName && i.CanStack(addedItem.Count))
                {
                    int stackAmount = Math.Min(i.HowMuchCanStack(), addedItem.Count);  // Стекуем максимальное возможное количество
                    i.IncreaseStack(stackAmount);
                    addedItem.DecreaseStack(stackAmount);
                    inventory.inventory[index] = i;  // Присваиваем изменённую структуру обратно
        
                    if (addedItem.Count == 0)
                    {
                        break;
                    }
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
            }
        }

        public static void AddItemFromOtherInventory(Inventory inventory, int itemIndex, int count)
        {
            var targetInventory = inventory.GetComponent<LootingSystem>().TargetInventory;
            InventoryItem chestItem = targetInventory.inventory[itemIndex];
            if (chestItem.ItemData == null) return;
            var remainingCount = chestItem.Count;
            if (count != 0)
            {
                remainingCount = 1;
            }

            for (var index = 0; index < inventory.inventory.Count; index++)
            {
                var _item = inventory.inventory[index];
                while (remainingCount > 0 && inventory.inventory[index].ItemName == chestItem.ItemName && inventory.inventory[index].CanStack(1))
                {
                    
                    _item.IncreaseStack(1);
                    chestItem.DecreaseStack(1);
                    inventory.inventory[index] = _item;
                    targetInventory.inventory[itemIndex] = chestItem;
                    remainingCount = remainingCount - 1;
                }
            }

            for (var index = 0; index < inventory.inventory.Count; index++)
            {
                if (inventory.inventory[index].ItemName == "Item" && remainingCount != 0)
                {
                    var _item =  new InventoryItem(chestItem.ItemData, chestItem.ItemData.ItemName, remainingCount);
                    inventory.inventory[index] = _item;
                    chestItem.Count -= remainingCount;
                    targetInventory.inventory[itemIndex] = chestItem;
                    remainingCount = 0;
                }
            }
        }

        public static void AddItemFromOtherInventoryForIndex(Inventory inventory, int itemIndex, int slotIndex, int count)
        {
            var targetInventory = inventory.GetComponent<LootingSystem>().TargetInventory;
            InventoryItem chestItem = targetInventory.inventory[itemIndex];
            
            if (chestItem.ItemData == null) return;

            var remainingCount = chestItem.Count;
            InventoryItem inventoryitem = inventory.inventory[slotIndex];

            if (inventoryitem.ItemName == "Item")
            {
                inventory.inventory[slotIndex] = new InventoryItem(chestItem);
                if (count != 0)
                {
                    var _item = inventory.inventory[slotIndex];
                    _item.Count = count;
                    inventory.inventory[slotIndex] = _item;
                    chestItem.DecreaseStack(count);
                }
                else if(count == 0) 
                {
                    chestItem.DecreaseStack(chestItem.Count);
                }

                targetInventory.inventory[itemIndex] = chestItem;
                return;
            }

            if (inventoryitem.ItemName == chestItem.ItemName && inventoryitem.HowMuchCanStack() != 0 && remainingCount != 0)
            {
                var canStackCount = inventoryitem.HowMuchCanStack();

                var stackCount = math.min(canStackCount, chestItem.Count);

                if (count != 0 && stackCount != 0)
                {
                    stackCount = 1;
                }

                inventoryitem.IncreaseStack(stackCount);
                inventory.inventory[slotIndex] = inventoryitem;
                chestItem.DecreaseStack(stackCount);
                targetInventory.inventory[itemIndex] = chestItem;
                remainingCount = remainingCount - stackCount;
            }
        }

        public static void PutItemToOtherInventoryByIndex(Inventory inventory, int itemIndex, int slotIndex, int count)
        {
            var targetInventory = inventory.GetComponent<LootingSystem>().TargetInventory;
            
            if (targetInventory == null) return;

            InventoryItem chestItem = targetInventory.inventory[slotIndex];
            InventoryItem inventoryitem = inventory.inventory[itemIndex];
            var remainingCount = inventoryitem.Count;
            if (chestItem.ItemName == "Item")
            {
                chestItem = new InventoryItem(inventoryitem);
                if (count != 0)
                {
                    chestItem.Count = count;
                    inventoryitem.DecreaseStack(count);
                }
                else if (count == 0)
                {
                    inventoryitem.DecreaseStack(inventoryitem.Count);
                }
                
                targetInventory.inventory[slotIndex] = chestItem;
                inventory.inventory[itemIndex] = inventoryitem;
                return;
            }

            if (inventoryitem.ItemName == targetInventory.inventory[slotIndex].ItemName && targetInventory.inventory[slotIndex].HowMuchCanStack() != 0 && remainingCount != 0)
            {
                var canStackCount = targetInventory.inventory[slotIndex].HowMuchCanStack();
                var stackCount = math.min(canStackCount, inventoryitem.Count);

                if (count != 0 && stackCount != 0)
                {
                    stackCount = 1;
                }

                chestItem = targetInventory.inventory[slotIndex];
                chestItem.IncreaseStack(stackCount);
                targetInventory.inventory[slotIndex] = chestItem;
                inventoryitem.DecreaseStack(stackCount);
                inventory.inventory[itemIndex] = inventoryitem;
                remainingCount = remainingCount - stackCount;
            }
        }

        public static void PutItemToOtherInventory(Inventory inventory, int itemIndex, int count)
        {
            Inventory targetInventory = inventory.GetComponent<LootingSystem>().TargetInventory;
            if (targetInventory == null) return;

            InventoryItem inventoryitem = inventory.inventory[itemIndex];    
            var remainingCount = inventoryitem.Count;
            if (count != 0) remainingCount = 1; ;   
        
            for (int i = 0; i < targetInventory.inventory.Count; i++)
            {
                if (inventoryitem.ItemName == targetInventory.inventory[i].ItemName && targetInventory.inventory[i].HowMuchCanStack() != 0 && remainingCount != 0)
                {
                    var canStackCount = targetInventory.inventory[i].HowMuchCanStack();
                    var stackCount = math.min(canStackCount, inventoryitem.Count);

                    if (count != 0 && stackCount != 0)
                    {
                        stackCount = 1;
                    }

                    var _item = targetInventory.inventory[i];
                    _item.IncreaseStack(stackCount);
                    targetInventory.inventory[i] = _item;
                    inventoryitem.DecreaseStack(stackCount);
                    inventory.inventory[itemIndex] = inventoryitem;                    
                    remainingCount = remainingCount - stackCount;
                    if (remainingCount == 0) return;
                }
            }

            if(remainingCount == 0) return;

            for (int i = 0; i < targetInventory.inventory.Count; i++)
            {
                if (targetInventory.inventory[i].ItemName == "Item" )
                {
                    targetInventory.inventory[i] = new InventoryItem(inventoryitem);

                    if (count != 0)
                    {
                        var _item = targetInventory.inventory[i];
                        _item.Count = count;
                        targetInventory.inventory[i] = _item;
                        inventoryitem.DecreaseStack(count);
                        inventory.inventory[itemIndex] = inventoryitem;       
                        return;
                    }
                    else if (count == 0)
                    {
                        inventoryitem.DecreaseStack(inventoryitem.Count);
                        inventory.inventory[itemIndex] = inventoryitem;    
                        return;
                    }
                }
            }
        }
    }
}
