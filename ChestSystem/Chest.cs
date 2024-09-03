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

    public override void OnStartServer()
    {
        base.OnStartServer();

        if (isServer)
        {
            Debug.Log("Server");
        }

        inventory = GetComponent<Inventory>();
        SpawnRandomLoot();
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
                Item currentItem = item.Item;
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
}
