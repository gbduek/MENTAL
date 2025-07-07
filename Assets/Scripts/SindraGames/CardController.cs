using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class RectTransformExtensions
{
    public static bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        Vector3[] corners1 = new Vector3[4];
        Vector3[] corners2 = new Vector3[4];

        rect1.GetWorldCorners(corners1);
        rect2.GetWorldCorners(corners2);

        Rect r1 = new Rect(corners1[0], corners1[2] - corners1[0]);
        Rect r2 = new Rect(corners2[0], corners2[2] - corners2[0]);

        return r1.Overlaps(r2);
    }
}

public class CardController
    : MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public Image image;

    [HideInInspector]
    public string cardID;
    private bool isMovable = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void SetupCard(string id)
    {
        cardID = id;

        // Example conditional logic:
        if (id == "Card1")
        {
            image.sprite = Resources.Load<Sprite>("Sprites/MemoryGame/king_of_hearts2");
        }
        else if (id == "Card2")
        {
            image.sprite = Resources.Load<Sprite>("Sprites/MemoryGame/queen_of_diamonds2");
            transform.position += new Vector3(-600, 0, 0); // Set the position of Card2
        }
        else if (id == "Card3")
        {
            image.sprite = Resources.Load<Sprite>("Sprites/MemoryGame/7_of_clubs");
            transform.position += new Vector3(-600, -300, 0); // Set the position of Card3
        }
        else
        {
            Debug.LogWarning("Unknown Card ID: " + id);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    private bool IsMatch(CardController other)
    {
        return (this.cardID == "Card2" && other.cardID == "Card1")
            || (this.cardID == "Card3" && other.cardID == "Card2");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (CardController otherCard in FindObjectsOfType<CardController>())
        {
            if (otherCard != this && IsOverlapping(otherCard))
            {
                Debug.Log("Overlapping with: " + otherCard.cardID);

                if (IsMatch(otherCard))
                {
                    Debug.Log("Match found between " + cardID + " and " + otherCard.cardID);
                    // Snap cards, play effect, etc.
                    return;
                }
            }
        }

        // If no match, return to original position
        rectTransform.anchoredPosition = originalPosition;
        isMovable = true; // Allow dragging again
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        isMovable = false;
    }

    public bool IsOverlapping(CardController other)
    {
        return RectTransformExtensions.IsOverlapping(rectTransform, other.rectTransform);
    }
}
