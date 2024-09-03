using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Canvas mainCanvas;
    private ItemDropController itemDropController;
    private RectTransform m_RectTransform;
    [HideInInspector] public Transform pareftAfterDrag;
    private Image image;
    private CanvasGroup canvasGroup;
    private GameObject dropZonePanel;

    private void Start()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        m_RectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
        itemDropController = mainCanvas.GetComponent<ItemDropController>();
        dropZonePanel = itemDropController.dropZone;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        dropZonePanel.SetActive(true); 
        pareftAfterDrag = transform.parent;
        transform.SetParent(mainCanvas.transform);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_RectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(pareftAfterDrag);
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        dropZonePanel.SetActive(false); 
    }
}
