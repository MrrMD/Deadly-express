using Mirror;
using System.Linq;
using UnityEngine;


[System.Serializable]
[RequireComponent(typeof(Inventory))]
public class Chest : NetworkBehaviour
{
    [SerializeField] private LootTable lootTable;
    [SerializeField] private int minLootCount;
    [SerializeField] private int maxLootCount;

    [SerializeField] Inventory inventory = null;

    public Inventory Inventory { get => inventory; set => inventory = value; }
    public override void OnStartServer()
    {
        base.OnStartServer();

        inventory = GetComponent<Inventory>();
        SpawnRandomLoot();
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public InventoryItem GetInventoryItemByIndex(int index)
    {
        return inventory.inventory[index];
    }

    [Server]
    public void SpawnRandomLoot()
    {
        int lootCount = Random.Range(minLootCount, maxLootCount);
        int currentLootCount = 0;
        while (currentLootCount <= lootCount)
        {
            ChestItem item = GetRandomItemFromLootList();

            if (item != null && inventory != null)
            {
                Item currentItem = new Item(item.Item);
                currentItem.Count = Random.Range(minLootCount, maxLootCount + 1);
                Debug.Log(currentItem.Count);

                inventory.AddItemToChest(currentItem);
                currentLootCount++;
            }
        }
    }

    private ChestItem GetRandomItemFromLootList()
    {
        int totalWeight = lootTable.LootList.Sum(item => item.Weight);
        int randomWeight = Random.Range(0, totalWeight);
        int currentWeight = 0;

        foreach (ChestItem item in lootTable.LootList)
        {
            currentWeight += item.Weight;
            if (randomWeight < currentWeight)
            {
                return item;
            }
        }
        return null;
    }

    [ClientRpc]
    public void RpcChestLootRemove(int index)
    {
        inventory.inventory[index].ItemName = "item";
        inventory.inventory[index].Count = 0;
        inventory.OnInventoryChanged();
    }

    [ClientRpc]
    public void RpcChestLootCountChange(int index, int count)
    {
        inventory.inventory[index].Count = count;
        inventory.OnInventoryChanged();
    }

}
