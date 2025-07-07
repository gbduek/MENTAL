using UnityEngine;
using UnityEngine.EventSystems;

public class MazeWall : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse touched the wall!");
        // TODO: Handle what should happen, anxiety, go back to bed...
    }
}
