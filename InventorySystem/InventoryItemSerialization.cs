using InventorySystem;
using Mirror;
using UnityEngine;

public static class InventoryItemSerialization
{
    public static void WriteInventoryItem(this NetworkWriter writer, InventoryItem item)
    {
        writer.WriteString(item.ItemName);
        writer.WriteInt(item.Count);
        writer.WriteString(item.ItemData != null ? item.ItemData.name : null);
    }

    public static InventoryItem ReadInventoryItem(this NetworkReader reader)
    {
        string itemName = reader.ReadString();
        int count = reader.ReadInt();
        string itemDataName = reader.ReadString();

        InventoryItem item = new InventoryItem
        {
            ItemName = itemName,
            Count = count,
            ItemData = !string.IsNullOrEmpty(itemDataName) ? Resources.Load<ItemData>("ScriptableObjects/items/" + itemDataName) : null
        };

        return item;
    }
}
