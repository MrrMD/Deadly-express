using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private string itemName;
    [SerializeField] private int count; // Количество предметов

    public ItemData ItemData { get => itemData; set => itemData = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public int Count
    {
        get => count;
        set
        {
            count = value;
            if (count <= 0)
            {
                count = 0;
                itemName = "Item";
                itemData = null;
            }
        }
    }

    // Конструктор структуры
    public InventoryItem(ItemData itemData, string itemName, int count)
    {
        this.itemData = itemData;
        this.itemName = itemName;
        this.count = count;
    }

    // Конструктор копирования
    public InventoryItem(InventoryItem inventoryItem)
    {
        this.itemData = inventoryItem.ItemData;
        this.itemName = inventoryItem.itemName;
        this.count = inventoryItem.count;
    }

    public bool CanStack(int itemsValue)
    {
        return count + itemsValue <= itemData.StackSize;
    }

    public int HowMuchCanStack()
    {
        return itemData.StackSize - count;
    }

    public void IncreaseStack(int value)
    {
        count += value;
    }

    public void DecreaseStack(int value)
    {
        count -= value;
        if (count <= 0)
        {
            count = 0;
            itemName = "Item";
            itemData = null;
        }
    }

    public override string ToString()
    {
        return $"Item: {itemName}, Count: {count}, ItemData: {itemData}";
    }
}