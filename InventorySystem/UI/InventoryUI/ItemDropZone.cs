using Mirror;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropZone : MonoBehaviour, IDropHandler
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

        if (eventData.pointerDrag.GetComponent<UIInventoryItem>() == null 
            || eventData.pointerDrag.GetComponent<UIInventoryLootItem>() != null)
        {
            return;
        }
        
        dragItem = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        Inventory inventory = identity.GetComponent<Inventory>();


        MakeItemNotVisible();

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.CmdRemoveItemByIndex(int.Parse(dragItem.name), 1);
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.CmdRemoveItemByIndex(int.Parse(dragItem.name), 0);
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
