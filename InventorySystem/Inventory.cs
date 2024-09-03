using Mirror;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    [SerializeField] public readonly SyncList<InventoryItem> inventory = new SyncList<InventoryItem>();
    [SerializeField] private int inventorySize = 6;
    public float chestDetectionRadius = 2.0f; // Радиус поиска ящика
    private void Awake()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (inventorySize == 0) inventorySize = 6;
    }

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        base.OnStartClient();

        LootUI.Instance.UpdateInventoryItems(GetAllItems());

        InitializeEmptyInventory();

        inventory.OnChange += OnInventoryChanged;
    }

    [Command]
    private void InitializeEmptyInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            var item = new InventoryItem();
            item.Count = 0;
            inventory.Add(item);
        }
        LootUI.Instance.UpdateInventoryItems(GetAllItems());
    }

    [Command]
    public void CmdRemoveItem(InventoryItem removedItem)
    {
        Debug.Log("CMD REMOVE ITEM");
        if (!HasEnoughItems(removedItem))
        {
            Debug.LogWarning("Недостаточное количество предметов для удаления.");
            return;
        }

        int remainingCountToRemove = removedItem.Count;
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();

        foreach (var _item in inventory)
        {
            if (_item.ItemName == removedItem.ItemName)
            {
                if (_item.Count > remainingCountToRemove)
                {
                    _item.DecreaseStack(remainingCountToRemove);
                    Debug.Log($"Уменьшен item {removedItem.ItemName}");
                    TargetRpcInventoryItemCountChange(inventory.IndexOf(_item), _item.Count);
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
            inventory.Remove(item);
            Debug.Log($"Удален item {item.ItemName}");

        }
    }

    [Command]
    public void CmdRemoveItemByIndex(int index, int count)
    {
        if(count != 0)
        {
            ItemSpawner.Instance.ServerSpawnItemByData(inventory[index].ItemData, count, transform.position + transform.forward * 1.0f);
            inventory[index].DecreaseStack(count);
            TargetRpcInventoryItemCountChange(inventory[index].Count, index);
            return;
        }
        ItemSpawner.Instance.ServerSpawnItemByData(inventory[index].ItemData, inventory[index].Count, transform.position + transform.forward * 1.0f);
        inventory.RemoveAt(index);
        var item = new InventoryItem();
        item.Count = 0;
        inventory.Insert(index, item);
    }

    [Command]
    public void CmdAddItem(Item addedItem)
    {
        foreach (var i in inventory)
        {
            while (addedItem.Count > 0 && i.ItemName == addedItem.ItemData.ItemName && i.CanStack(1))
            {
                i.IncreaseStack(1);
                addedItem.DecreaseStack(1);
                Debug.Log($"Добавлен 1 предмет, осталось {addedItem.Count}");
                TargetRpcInventoryItemCountChange(i.Count, inventory.IndexOf(i));
                break;
            }
            if (addedItem.Count == 0)
            {
                Debug.Log("Все предметы добавлены в инвентарь");
                break;
            }
        }

        foreach (var i in inventory)
        {
            if (i.ItemName == "Item" && addedItem.Count != 0)
            {
                inventory[inventory.IndexOf(i)] = new InventoryItem(addedItem.ItemData, addedItem.ItemData.ItemName, addedItem.Count);
                Debug.Log($"Добавлен новый предмет в инвентарь, количество: {addedItem.Count}");
                addedItem.DecreaseStack(addedItem.Count);
                return;
            }
            else if(addedItem.Count != 0)
            {
                Debug.Log($"Не удалось добавить в инвентарь {addedItem.Count} предметов");
            }
        }
    }

    [Command]
    public void addItemFromChest(int itemIndex, int slotIndex)
    {
        int remainingCount = 0;

        Chest chest = FindNearbyChest();

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);
        chest = null;

        if (item == null) return;

        remainingCount = item.Count;
         
        foreach (var i in inventory) 
        {
            if(i.ItemName == "Item") continue;

            if (i.ItemName == item.ItemName && i.HowMuchCanStack() != 0 && remainingCount != 0)
            {
                var canStackCount = i.HowMuchCanStack();
                var StackCount = math.min(canStackCount, item.Count);
                Debug.Log(canStackCount);
               
                i.IncreaseStack(StackCount);
                item.DecreaseStack(StackCount);
                RpcInventoryLootUIUpdate();
                TargetRpcInventoryItemCountChange(i.Count, inventory.IndexOf(i));
                remainingCount = remainingCount - StackCount;
            }
        }
        if (remainingCount != 0)
        {
            foreach (var i in inventory)
            {
                if(i.ItemName == "Item")
                {
                    inventory[inventory.IndexOf(i)] = new InventoryItem(item);
                    item.DecreaseStack(item.Count);
                    RpcInventoryLootUIUpdate();
                    remainingCount = 0;
                    break;
                }
            }
        }
    }

    [Command]
    public void addItemFromChestForIndex(int itemIndex, int slotIndex)
    {
        int remainingCount = 0;

        Chest chest = FindNearbyChest();

        if (chest == null) return;

        InventoryItem item = chest.GetInventoryItemByIndex(itemIndex);
        chest = null;

        if (item == null) return;

        remainingCount = item.Count;

        InventoryItem inventoryitem = inventory[slotIndex];

        if (inventoryitem.ItemName == "Item")
        {
            inventory[slotIndex] = new InventoryItem(item);
            item.DecreaseStack(item.Count);
            RpcInventoryLootUIUpdate();
            remainingCount = 0;
            return;
        }

        if (inventoryitem.ItemName == item.ItemName && inventoryitem.HowMuchCanStack() != 0 && remainingCount != 0)
        {
            var canStackCount = inventoryitem.HowMuchCanStack();
            var StackCount = math.min(canStackCount, item.Count);
            Debug.Log(canStackCount);

            inventoryitem.IncreaseStack(StackCount);
            item.DecreaseStack(StackCount);
            RpcInventoryLootUIUpdate();
            TargetRpcInventoryItemCountChange(inventoryitem.Count, slotIndex);
            remainingCount = remainingCount - StackCount;
        }
    }

    [Server]
    public void AddItemToChest(Item addedItem)
    {
        inventory.Add(new InventoryItem(addedItem.ItemData, addedItem.ItemData.ItemName, addedItem.Count));
    }

    public Chest FindNearbyChest()
    {
        // Получаем все коллайдеры, попавшие в радиус
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chestDetectionRadius);

        // Перебираем найденные объекты
        foreach (Collider hitCollider in hitColliders)
        {
            // Проверяем, есть ли у объекта компонент Chest
            Chest chest = hitCollider.GetComponent<Chest>();
            if (chest != null)
            {
                return chest; // Возвращаем первый найденный ящик
            }
        }

        return null; // Возвращаем null, если ящик не найден
    }

    [Command] 
    public void CmdMoveItemStack(int from, int to)
    {
        Debug.Log("Cmd move item");

        if (inventory[to].ItemName == "Item" || inventory[to].ItemName != inventory[from].ItemName 
            || (inventory[from].ItemName == inventory[to].ItemName && !inventory[to].CanStack(1)))
        {
            InventoryItem item = inventory[from];
            inventory[from] = inventory[to];
            inventory[to] = item;
            return;
        }

        var firstitemCount = inventory[from].Count;

        for (int i = 0; i < firstitemCount; i++)
        {
            if (inventory[from].ItemName == inventory[to].ItemName && inventory[to].CanStack(1))
            {
                inventory[to].IncreaseStack(1);
                inventory[from].DecreaseStack(1);
                TargetRpcInventoryItemCountChange(inventory[to].Count, to);
                TargetRpcInventoryItemCountChange(inventory[from].Count, from);
            }
        }
    }

    [Command]
    public void CmdMoveOneItem(int from, int to)
    {
        if(inventory[from].Count == 0)
        {
            return;
        }

        if (inventory[to].ItemName == "Item")
        {
            InventoryItem item = new InventoryItem(inventory[from]);
            inventory[from].DecreaseStack(1);
            TargetRpcInventoryItemCountChange(inventory[from].Count, from);
            item.Count = 1;
            inventory[to] = item;
        }
        else if (inventory[from].ItemName == inventory[to].ItemName && inventory[to].CanStack(1))
        {
            Debug.Log("Cmd move one item");

            inventory[to].IncreaseStack(1);
            inventory[from].DecreaseStack(1);
            TargetRpcInventoryItemCountChange(inventory[to].Count, to);
            TargetRpcInventoryItemCountChange(inventory[from].Count, from);
        }
        else
        {
            UpdateInventoryUI();
        }
    }

    public bool HasEnoughItems(InventoryItem requiredItem)
    {
        if (requiredItem == null) return false;

        int totalCount = 0;

        foreach (var item in inventory)
        {
            if (item.ItemName == requiredItem.ItemName)
            {
                totalCount += item.Count;
                if (totalCount >= requiredItem.Count) return true;
            }
        }
        return false;
    }

    private void OnInventoryChanged(SyncList<InventoryItem>.Operation operation, int arg2, InventoryItem item)
    {
        if (!isLocalPlayer) return;
        UpdateInventoryUI();
    }

    [TargetRpc]
    private void TargetRpcInventoryItemCountChange(int count, int index)
    {
        if (!isLocalPlayer) return;
        Debug.Log("TargetRpcInventoryItemChanged");
        inventory[index].Count = count;
        UpdateInventoryUI();
    }

    [TargetRpc]
    private void RpcInventoryLootUIUpdate()
    {
        if (!isLocalPlayer) return;
        UpdateInventoryLootUI();
    }

    [TargetRpc]
    private void RpcUpdateInventoryUI()
    {
        if (!isLocalPlayer) return;
        UpdateInventoryUI();
    }

    private void UpdateInventoryLootUI()
    {
        LootUI.Instance.UpdateLootItems();
    }

    private void UpdateInventoryUI()
    {
        Debug.Log("InventoryUIUpdate");
        LootUI.Instance.UpdateInventoryItems(GetAllItems());
    }

    public List<InventoryItem> GetAllItems()
    {
        Debug.Log("GetAllitems " + inventory.Count);
        return inventory.OfType<InventoryItem>().ToList();
    }
}
