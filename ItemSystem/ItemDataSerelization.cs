using Mirror;
using UnityEngine;

public static class ItemDataSerelization
{
    public static void WriteInventoryItem(this NetworkWriter writer, ItemData ItemData)
    {
        writer.WriteString(ItemData.ItemName);
    }

    public static InventoryItem ReadInventoryItem(this NetworkReader reader)
    {
        string itemName = reader.ReadString();

        InventoryItem item = new InventoryItem
        {
            ItemName = itemName,
      
            ItemData = !string.IsNullOrEmpty(itemName) ? Resources.Load<ItemData>("ScriptableObjects/items/" + itemName) : null
        };

        return item;
    }
}
