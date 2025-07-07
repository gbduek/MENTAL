using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
public class DraggableFile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool isDragging = false;

    public Vector2 originalPosition;
    public RectTransform originalParent;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas parentCanvas;

    private int originalSiblingIndex;
    private Vector3 originalScale;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        originalPosition = rectTransform.anchoredPosition;
        originalParent = rectTransform.parent as RectTransform;
        originalSiblingIndex = rectTransform.GetSiblingIndex();
        originalScale = rectTransform.localScale;

        canvasGroup.blocksRaycasts = false;

        // Mover para o topo do canvas
        rectTransform.SetParent(parentCanvas.transform, true);
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true;
    }

    public void ReturnToOriginalPosition()
    {
        // Restaurar parent e ordem
        rectTransform.SetParent(originalParent, true);
        rectTransform.SetSiblingIndex(originalSiblingIndex);

        // Restaurar posição e escala
        rectTransform.anchoredPosition = originalPosition;
        rectTransform.localScale = originalScale;
    }
}
