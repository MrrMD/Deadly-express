using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Utils;

namespace InventorySystem
{
    public class Inventory : NetworkBehaviour
    {
        public event Action OnInventoryChangedEvent;

        [SerializeField] public readonly SyncList<InventoryItem> inventory = new SyncList<InventoryItem>();
        private int inventorySize = InventoryConstants.INVENTORY_SIZE;

        public override void OnStartClient()
        {
            if (!isLocalPlayer && !GetComponent<Chest>())
            {
                return;
            }
            inventory.OnChange += OnInventoryChanged;

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
            UpdateInventoryUI();
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
        public void CmdAddItemFromOtherInventory(int itemIndex, int count)
        {
            InventoryAddItem.AddItemFromOtherInventory(this, itemIndex, count);
        }

        [Command]
        public void CmdAddItemFromOtherInventoryForIndex(int itemIndex, int slotIndex, int count)
        {
            InventoryAddItem.AddItemFromOtherInventoryForIndex(this, itemIndex, slotIndex, count); 
        }

        [Command]
        public void CmdPutItemToOtherInventory(int itemIndex, int count)
        {
            InventoryAddItem.PutItemToOtherInventory(this, itemIndex, count);
        }

        [Command]
        public void CmdPutItemToOtherInventoryByIndex(int itemIndex, int slotIndex, int count)
        {
            InventoryAddItem.PutItemToOtherInventoryByIndex(this, itemIndex, slotIndex, count);
        }

        [Server]
        public void AddItem(Item addedItem)
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

        public void UpdateInventoryUI()
        {
            if(!isLocalPlayer) return;
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
}