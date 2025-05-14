using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " +  collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
