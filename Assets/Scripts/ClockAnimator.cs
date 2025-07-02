using UnityEngine;

public class ClockAnimator : MonoBehaviour
{
    public string spriteFolder = "ClockSprites"; // Pasta dentro de Resources
    public float frameRate = 1f; // Segundos por frame (1f = 1 segundo)

    private Sprite[] clockSprites;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer;

    void Start()
    {
        // Carrega os sprites da pasta Resources/ClockSprites
        clockSprites = Resources.LoadAll<Sprite>(spriteFolder);
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (clockSprites.Length == 0)
        {
            Debug.LogError("Nenhum sprite foi encontrado na pasta Resources/" + spriteFolder);
            return;
        }

        spriteRenderer.sprite = clockSprites[currentFrame];
    }

    void Update()
    {
        if (clockSprites.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % clockSprites.Length;
            spriteRenderer.sprite = clockSprites[currentFrame];
        }
    }
}
