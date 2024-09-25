using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    public event Action OnInventoryChangedEvent;

    [SerializeField] public readonly SyncList<InventoryItem> inventory = new SyncList<InventoryItem>();
    [SerializeField] private int inventorySize = 6;

    public override void OnStartClient()
    {
        inventory.OnChange += OnInventoryChanged;

        if (!isLocalPlayer)
        {
            return;
        }

        if (inventorySize == 0) inventorySize = 6;

        base.OnStartClient();

        InitializeEmptyInventory();
    }

    [Command]
    private void InitializeEmptyInventory()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = new InventoryItem();
            item.Count = 0;
            inventory.Add(item);
        }
        LootUI.Instance.UpdateInventoryItems(GetAllItems());
    }

    [Command]
    public void CmdRemoveItem(InventoryItem removedItem)
    {
        InventoryRemoveItem.RemoveItem(this, removedItem);
    }

    [Command]
    public void CmdRemoveItemByIndexAndSpawn(int index, int count)
    {
        InventoryRemoveItem.RemoveItemByIndexAndSpawn(this, index, count);
    }

    [Command] 
    public void CmdMoveItemStack(int from, int to)
    {
        InventoryItemMove.MoveItemStack(this, from, to);
    }

    [Command]
    public void CmdAddItem(Item addedItem)
    {
        InventoryAddItem.AddItem(this, addedItem);
    }

    [Command]
    public void CmdAddItemFromChest(int itemIndex, int count)
    {
        InventoryAddItem.AddItemFromChest(this, itemIndex, count);
    }

    [Command]
    public void CmdAddItemFromChestForIndex(int itemIndex, int slotIndex, int count)
    {
        InventoryAddItem.AddItemFromChestForIndex(this, itemIndex, slotIndex, count); 
    }

    [Command]
    public void CmdPutItemToChest(int itemIndex, int count)
    {
        InventoryAddItem.AddItemToChest(this, itemIndex, count);
    }

    [Command]
    public void CmdPutItemToChestByIndex(int itemIndex, int slotIndex, int count)
    {
        InventoryAddItem.AddItemToChestByIndex(this, itemIndex, slotIndex, count);
    }

    [Server]
    public void AddItemToChest(Item addedItem)
    {
        inventory.Add(new InventoryItem(addedItem.ItemData, addedItem.ItemData.ItemName, addedItem.Count));
    }

    [Command]
    public void CmdMoveOneItem(int from, int to)
    {
        InventoryItemMove.MoveOneItem(this, from, to);
    }

    private void OnInventoryChanged(SyncList<InventoryItem>.Operation operation, int arg2, InventoryItem item)
    {
        Debug.Log("OnInventoryChanged");
        OnInventoryChanged();
    }

    public void OnInventoryChanged()
    {
        if (GetComponent<Chest>() != null)
        {
            if (OnInventoryChangedEvent != null)
            {
                Debug.Log("Invoke");
                OnInventoryChangedEvent.Invoke();
            }
        }
        if (!isLocalPlayer) return;

        UpdateInventoryUI();
    }

    [TargetRpc]
    public void TargetRpcInventoryItemCountChange(int count, int index)
    {
        if (!isLocalPlayer) return;
        Debug.Log("TargetRpcInventoryItemCountChange");
        inventory[index].Count = count;
        UpdateInventoryUI();
    }

    [ClientRpc]
    public void RpcInventoryLootUIUpdate()
    {
        Debug.Log("RpcInventoryLootUIUpdate");
        UpdateInventoryLootUI();
    }

    [TargetRpc]
    public void RpcUpdateInventoryUI()
    {
        Debug.Log("RpcUpdateInventoryUI");
        UpdateInventoryUI();
    }

    public void UpdateInventoryLootUI()
    {
        LootUI.Instance.ShowLootItems();
    }

    public void UpdateInventoryUI()
    {
        LootUI.Instance.UpdateInventoryItems(GetAllItems());
    }

    public List<InventoryItem> GetAllItems()
    {
        return inventory.OfType<InventoryItem>().ToList();
    }

    [TargetRpc]
    public void RpcCloseLootUI()
    {
        LootUI.Instance.Close();
    }
    public override string ToString()
    {
        return $"Inventory Count: {inventory.Count}";
    }
}