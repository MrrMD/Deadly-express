using Mirror;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;

public class LootUI : MonoBehaviour
{
    public static LootUI Instance { get; private set; }

    [SerializeField] private GameObject lootPanel;
    [SerializeField] private GameObject InventorySlotPrefab;
    [SerializeField] private GameObject InventoryItemPrefab;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryLootSlotPrefab;
    [SerializeField] private GameObject inventoryLootItemPrefab;
    [SerializeField] private bool lootUiIsOpen = false;
    [SerializeField] private bool isInventoryOpen = false;

    private Inventory lootingInventory;
    public Inventory LootingInventory { get => lootingInventory; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lootPanel.SetActive(false);
    }

    public void Open(Inventory inventory)
    {
        Debug.Log("Loot UI open");
        lootUiIsOpen = true;
        NetworkClient.localPlayer.GetComponent<Player.Player>().PlayerCameraController.LockCamera();
        lootingInventory = inventory;
        lootPanel.SetActive(true);
        ShowLootItems();
        inventory.OnInventoryChangedEvent += ShowLootItems;
    }

    public void Close()
    {
        NetworkClient.localPlayer.GetComponent<Player.Player>().PlayerCameraController.UnlockCamera();
        lootingInventory.OnInventoryChangedEvent -= ShowLootItems;
        lootUiIsOpen = false;
        lootPanel.SetActive(false);
        lootingInventory = null;
    }

    public void ShowLootItems()
    {
        if (!lootUiIsOpen) return;
        Debug.Log("Update loot items");

        foreach (Transform child in lootPanel.transform)
        {
            Destroy(child.gameObject);
        }

        List<InventoryItem> lootItems = lootingInventory.GetAllItems();
        for (int i = 0; i < lootItems.Count; i++)
        {
            GameObject lootItemSlot = Instantiate(inventoryLootSlotPrefab, lootPanel.transform);
            lootItemSlot.name = i.ToString();
            GameObject lootItem = Instantiate(inventoryLootItemPrefab, lootItemSlot.transform);
            lootItem.name = i.ToString();
            lootItem.GetComponentInChildren<TextMeshProUGUI>().text = lootItems[i].Count.ToString();
            if (lootItems[i].ItemName == "Item")
            {
                lootItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
                lootItem.SetActive(false);
            }
        }
    }

    public void UpdateInventoryItems(List<InventoryItem> inventory)
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject inventorySlot = Instantiate(InventorySlotPrefab, inventoryPanel.transform);
            inventorySlot.name = i.ToString();
            GameObject inventoryItem = Instantiate(InventoryItemPrefab, inventorySlot.transform);
            inventoryItem.name = i.ToString();
            //inventoryItem.GetComponent<Image>().sprite = item.ItemIcon;
            if (inventory[i].ItemName == "Item")
            {
                inventoryItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
                inventoryItem.SetActive(false);
            }
            inventoryItem.GetComponentInChildren<TextMeshProUGUI>().text = inventory[i].Count.ToString();
        }
    }

    private void OpenInventory()
    {
        isInventoryOpen = true;
        NetworkClient.localPlayer.GetComponent<Player.Player>().PlayerCameraController.LockCamera();
    }

    private void CloseInventory()
    {
        isInventoryOpen = false;
        NetworkClient.localPlayer.GetComponent<Player.Player>().PlayerCameraController.UnlockCamera();
    }

    public void OnGUI()
    {
        if ((Event.current.Equals(Event.KeyboardEvent("Tab")) && !isInventoryOpen) && !lootUiIsOpen)
        {
            OpenInventory();
            return;
        }
        if (Event.current.Equals(Event.KeyboardEvent("Tab")) && lootUiIsOpen)
        {
            lootingInventory.GetComponent<LootingSystem>().CmdSetLootingInactive();
        }
        if (Event.current.Equals(Event.KeyboardEvent("Tab")) && isInventoryOpen)
        {
            CloseInventory();
        }
    }
}