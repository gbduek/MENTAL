using UnityEngine;

public class PlayerTimelineHooks : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void FaceLeft()
    {
        spriteRenderer.flipX = false;
    }

    public void FaceRight()
    {
        spriteRenderer.flipX = true;
    }

    public void SetWaking()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("IsWaking", false);
        }
    }
}
