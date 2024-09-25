using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryLootSlot : MonoBehaviour, IDropHandler
{
    NetworkIdentity identity;
    private UIInventoryItem dragItem;
    [SerializeField] private Inventory lootingInventory;
    [SerializeField] Inventory playerinventory;


    private void Start()
    {
        if (NetworkClient.localPlayer != null)
        {
            identity = NetworkClient.localPlayer.GetComponent<NetworkIdentity>();
        }
        else
        {
            Debug.LogWarning("NetworkClient.localPlayer is null.");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (eventData.pointerDrag.GetComponent<UIInventoryItem>() != null)
        {
            lootingInventory = LootUI.Instance.LootingInventory;
            if (lootingInventory == null)
            {
                Debug.Log("Looting inventory  == null");
                return;
            }
            InventoryToLootIteract(eventData);
        }
    }

    private void InventoryToLootIteract(PointerEventData eventData)
    {
        int inventoryItemIndex = int.Parse(eventData.pointerDrag.name);
        playerinventory = identity.GetComponent<Inventory>();

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            identity.GetComponent<PlayerItemSystem>().PutItemToChest(inventoryItemIndex, int.Parse(gameObject.name), 0);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            identity.GetComponent<PlayerItemSystem>().PutItemToChest(inventoryItemIndex, int.Parse(gameObject.name), 1);
        }
    }
}
