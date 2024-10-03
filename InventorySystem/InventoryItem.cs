using UnityEngine;

namespace InventorySystem
{
    [System.Serializable]
    public class InventoryItem
    {
        [SerializeField] private ItemData itemData = null;
        [SerializeField] private string itemName = "Item";
        [SerializeField] private int count = 0; // Количество предметов

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

        public InventoryItem()
        {
        }

        public InventoryItem(ItemData itemData, string itemName, int count)
        {
            this.itemData = itemData;
            this.itemName = itemName;
            this.count = count;
        }

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

        public void IncreaseStack(int value) { count += value; }

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
            return $"Item: {itemName}, Count: {count}, ItemData{itemData}";
        }
    }
}