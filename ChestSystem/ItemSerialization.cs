using Mirror;
using UnityEngine;

public static class ItemSerialization 
{
    public static void WriteItem(this NetworkWriter writer, Item item)
    {
        writer.WriteInt(item.Count);
        writer.WriteString(item.ItemData != null ? item.ItemData.name : null);

        writer.WriteNetworkIdentity(item.GetComponent<NetworkIdentity>());
    }

    public static Item ReadItem(this NetworkReader reader)
    {
        int count = reader.ReadInt();
        string itemDataName = reader.ReadString();
        NetworkIdentity identity = reader.ReadNetworkIdentity();
        Item item = new Item();
        if (identity != null)
        {
            item = identity.GetComponent<Item>();
        }
        item.Count = count;
        item.ItemData = !string.IsNullOrEmpty(itemDataName) ? Resources.Load<ItemData>("ScriptableObjects/items/" + itemDataName) : null;

        return item;
    }
}
