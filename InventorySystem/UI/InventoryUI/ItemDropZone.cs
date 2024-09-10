using Mirror;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] NetworkIdentity identity;
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
        Inventory inventory = null;

        if (eventData.pointerDrag.GetComponent<UIInventoryLootItem>())
        {
            return;
        }
        else if (eventData.pointerDrag.GetComponent<UIInventoryItem>() != null 
            && eventData.pointerDrag.GetComponent<UIInventoryLootItem>() == null)
        {
            inventory = identity.GetComponent<Inventory>();
        }

        dragItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        int itemName = int.Parse(dragItem.name);


        MakeItemNotVisible();

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.CmdRemoveItemByIndexAndSpawn(int.Parse(dragItem.name), 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.CmdRemoveItemByIndexAndSpawn(int.Parse(dragItem.name), 0);
        }

    }

    private void MakeItemNotVisible()
    {
        Color color = dragItem.GetComponent<Image>().color;
        color.a = 0;
        dragItem.GetComponent<Image>().color = color;
        dragItem.GetComponentInChildren<TextMeshProUGUI>().color = color;
    }
}
