using Mirror;
using UnityEngine;

[System.Serializable]
public class Item : NetworkBehaviour
{
    [SerializeField] private ItemData itemData; // Ссылка на ScriptableObject
    [SerializeField] private int count = 1; // Количество предметов

    public Item()
    {
    }

    public Item(ItemData itemData, int count)
    {
        this.itemData = itemData;
        this.count = count;
    }

    public Item(InventoryItem inventoryItem)
    {
        this.itemData = inventoryItem.ItemData;
        this.count = inventoryItem.Count;
    }

    public Item(Item item)
    {
        this.itemData = item.itemData;
        this.count = item.count;
    }

    public ItemData ItemData { get => itemData; set => itemData = value; }
    public int Count { get => count; set => count = value; }

    public void DecreaseStack(int value) 
    { 
        count -= value;
        if (count <= 0)
        {
            count = 0;
            try{
                NetworkServer.Destroy(gameObject);
            }
            catch
            {
            }
            

        }
    }
}
