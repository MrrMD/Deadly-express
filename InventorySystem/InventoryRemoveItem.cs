using Mirror;
using System.Collections.Generic;
using InventorySystem;
using ItemSystem;
using UnityEngine;

public static class InventoryRemoveItem 
{
    public static void RemoveItem(Inventory inventory, InventoryItem removedItem)
    {
        if (!Utils.InventoryUtils.HasEnoughItems(inventory, removedItem))
        {
            Debug.LogWarning("Недостаточное количество предметов для удаления.");
            return;
        }

        int remainingCountToRemove = removedItem.Count;
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();

        foreach (var _item in inventory.inventory)
        {
            if (_item.ItemName == removedItem.ItemName)
            {
                if (_item.Count > remainingCountToRemove)
                {
                    _item.DecreaseStack(remainingCountToRemove);
                    remainingCountToRemove = 0;
                    break;
                }
                else
                {
                    remainingCountToRemove -= _item.Count;
                    itemsToRemove.Add(_item);
                    if (remainingCountToRemove == 0) break;
                }
            }
        }

        foreach (var item in itemsToRemove)
        {
            inventory.inventory.Remove(item);
        }
    }

    public static void RemoveItemByIndexAndSpawn(Inventory inventory, int index, int count)
    {
        if (count != 0)
        {
            Debug.Log(inventory);
            ItemSpawner.Instance.ServerSpawnItemByData(inventory.inventory[index].ItemData, count, inventory.gameObject.transform.position + inventory.gameObject.transform.forward * 1.0f);
            var _item = inventory.inventory[index];
            _item.DecreaseStack(count);
            inventory.inventory[index] = _item;
            return;
        }
        ItemSpawner.Instance.ServerSpawnItemByData(inventory.inventory[index].ItemData, inventory.inventory[index].Count, inventory.transform.position + inventory.transform.forward * 1.0f);
        inventory.inventory.RemoveAt(index);
        var item = new InventoryItem();
        item.Count = 0;
        inventory.inventory.Insert(index, item);
    }
}
