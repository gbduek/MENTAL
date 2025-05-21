using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    private float movementX;
    private float movementY;
    private int count;
    public int lives = 5;
    public KarmaController playerKarma;

    void Start()
    {
        // Initialization code if needed
        playerKarma.getKarma();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        playerKarma.setKarma(0.1f);
    }

    private void Update()
    {
        Vector2 movement = new Vector2(movementX, movementY).normalized;
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
