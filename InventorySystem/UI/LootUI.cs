using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LootUI : MonoBehaviour
{
    public static LootUI Instance { get; private set; }

    [SerializeField] private GameObject lootPanel;
    [SerializeField] private GameObject UIInventorySlotPrefab;
    [SerializeField] private GameObject UIInventoryItemPrefab;
    [SerializeField] private GameObject inventoryPanel; 
    [SerializeField] private GameObject inventoryLootItemPrefab; 

    private Inventory lootingInventory;

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

        this.lootingInventory = inventory;

        lootPanel.SetActive(true);
        UpdateLootItems();
    }

    public void Close()
    {
        lootPanel.SetActive(false);
        lootingInventory = null;
    }

    public void UpdateLootItems()
    {
        foreach (Transform child in lootPanel.transform)
        {
            Destroy(child.gameObject);
        }

        List<InventoryItem> lootItems = lootingInventory.GetAllItems();
        for (int i = 0; i < lootItems.Count; i++)
        {
            GameObject lootItemSlot = Instantiate(UIInventorySlotPrefab, lootPanel.transform);
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
        Debug.Log($"UI Update inventory count: {inventory.Count}");

        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < inventory.Count; i++)
        {
            GameObject inventorySlot = Instantiate(UIInventorySlotPrefab, inventoryPanel.transform);
            inventorySlot.name = i.ToString();
            GameObject inventoryItem = Instantiate(UIInventoryItemPrefab, inventorySlot.transform);
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

}