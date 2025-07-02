using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private float typingSpeed = 0.05f;

    private void Start()
    {
        // Start the dialogue
        StartCoroutine(TypeDialogue("Hello, welcome to the game!"));
    }

    private void Update() { }

    private IEnumerator TypeDialogue(string message)
    {
        dialogueText.text = "";
        foreach (char letter in message)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(2f); // Wait for 2 seconds before clearing the dialogue
        gameObject.SetActive(false);
    }
}
