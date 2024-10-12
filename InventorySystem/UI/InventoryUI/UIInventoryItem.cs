using InventorySystem;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Canvas mainCanvas;
    private ItemDropController itemDropController;
    private RectTransform m_RectTransform;
    [HideInInspector] public Transform pareftAfterDrag;
    private Image image;
    private CanvasGroup canvasGroup;
    private GameObject dropZonePanel;
    private bool isDragging = false;
    private TextMeshProUGUI countText;

    private void Start()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        m_RectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        itemDropController = mainCanvas.GetComponent<ItemDropController>();
        dropZonePanel = itemDropController.dropZone;
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        dropZonePanel.SetActive(true);
        countText.gameObject.SetActive(false);
        pareftAfterDrag = transform.parent;
        transform.SetParent(mainCanvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        m_RectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(pareftAfterDrag);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        dropZonePanel.SetActive(false);
        isDragging = false;
        countText.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var count = 0; 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            count = 0;
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            count = 1;
        }

        if (!isDragging && GetComponent<UIInventoryLootItem>() != null)
        {
            NetworkClient.localPlayer.GetComponent<Inventory>().CmdAddItemFromOtherInventory(int.Parse(eventData.pointerDrag.name), count);
        }
        if (!isDragging && GetComponent<UIInventoryLootItem>() == null)
        {
            NetworkClient.localPlayer.GetComponent<Inventory>().CmdPutItemToOtherInventory(int.Parse(eventData.pointerDrag.name), count);
        }

    }

}
