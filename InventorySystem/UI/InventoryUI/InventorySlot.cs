using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    NetworkIdentity identity;
    private UIInventoryItem dragItem;

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
        if (eventData.pointerDrag.GetComponent<UIInventoryItem>() != null && eventData.pointerDrag.GetComponent<UIInventoryLootItem>() == null)
        {
            InventoryIteract(eventData);
        }
        else if (eventData.pointerDrag.GetComponent<UIInventoryLootItem>())
        {
            LootIteract(eventData);
        }
    }

    private void LootIteract(PointerEventData eventData)
    {
        dragItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        Inventory inventory = identity.GetComponent<Inventory>();

        if (transform.childCount == 0)
        {
            dragItem.pareftAfterDrag = transform;
        }
        else
        {
            Transform _transform = dragItem.pareftAfterDrag;
            Transform child = transform.GetChild(0);

            if (NetworkClient.localPlayer != null && NetworkClient.localPlayer.isLocalPlayer)
            {
                //MakeItemNotVisible();

                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    inventory.addItemFromChestForIndex(int.Parse(dragItem.name), int.Parse(gameObject.name));
                }
                else
                {
                    
                }
            }
            else
            {
                Debug.LogWarning("Попытка вызова команды без авторизации.");
            }
        }
    }

    private void InventoryIteract(PointerEventData eventData)
    {
        dragItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        Inventory inventory = identity.GetComponent<Inventory>();

        if (transform.childCount == 0)
        {
            dragItem.pareftAfterDrag = transform;
        }
        else
        {
            Transform _transform = dragItem.pareftAfterDrag;
            Transform child = transform.GetChild(0);

            if (NetworkClient.localPlayer != null && NetworkClient.localPlayer.isLocalPlayer)
            {
                MakeItemNotVisible();

                if (eventData.button == PointerEventData.InputButton.Right)
                {
                    Debug.Log("Moving with right btn");
                    inventory.CmdMoveOneItem(int.Parse(dragItem.name), int.Parse(child.name));
                }
                else
                {
                    inventory.CmdMoveItemStack(int.Parse(dragItem.name), int.Parse(child.name));
                }
            }
            else
            {
                Debug.LogWarning("Попытка вызова команды без авторизации.");
            }
        }
    }
    //что бы убрать лаг при перемещении
    private void MakeItemNotVisible()
    {
        Color color = dragItem.GetComponent<Image>().color;
        color.a = 0;
        dragItem.GetComponent<Image>().color = color;
        dragItem.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }
}
