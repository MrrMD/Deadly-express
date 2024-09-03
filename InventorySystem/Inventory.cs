using Mirror;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InventoryAddItem))]
public class Inventory : NetworkBehaviour
{
    [SerializeField] public readonly SyncList<InventoryItem> inventory = new SyncList<InventoryItem>();
    [SerializeField] private int inventorySize = 6;

    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (inventorySize == 0) inventorySize = 6;

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
        InventoryRemoveItem.RemoveItem(this, removedItem);
    }

    [Command]
    public void CmdRemoveItemByIndex(int index, int count)
    {
        InventoryRemoveItem.RemoveItemByIndex(this, index, count);
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
    public void addItemFromChest(int itemIndex, int slotIndex)
    {
        InventoryAddItem.addItemFromChest(this, itemIndex, slotIndex);
    }

    [Command]
    public void addItemFromChestForIndex(int itemIndex, int slotIndex)
    {
        InventoryAddItem.addItemFromChestForIndex(this, itemIndex, slotIndex); 
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
        if (!isLocalPlayer) return;
        UpdateInventoryUI();
    }

    [TargetRpc]
    public void TargetRpcInventoryItemCountChange(int count, int index)
    {
        if (!isLocalPlayer) return;
        inventory[index].Count = count;
        UpdateInventoryUI();
    }

    [TargetRpc]
    public void RpcInventoryLootUIUpdate()
    {
        if (!isLocalPlayer) return;
        UpdateInventoryLootUI();
    }

    [TargetRpc]
    public void RpcUpdateInventoryUI()
    {
        if (!isLocalPlayer) return;
        UpdateInventoryUI();
    }

    public void UpdateInventoryLootUI()
    {
        LootUI.Instance.UpdateLootItems();
    }

    public void UpdateInventoryUI()
    {
        LootUI.Instance.UpdateInventoryItems(GetAllItems());
    }

    public List<InventoryItem> GetAllItems()
    {
        return inventory.OfType<InventoryItem>().ToList();
    }

    public override string ToString()
    {
        return $"Inventory Count: {inventory.Count}";
    }

}