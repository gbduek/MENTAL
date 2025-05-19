using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour
{
    SpriteRenderer sr;
    const float pixelsPerUnit = 20f;

    void Awake() => sr = GetComponent<SpriteRenderer>();

    void LateUpdate()
    {
        sr.sortingOrder = -(int)(transform.position.y * pixelsPerUnit);
    }
}
