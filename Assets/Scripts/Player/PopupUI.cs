using UnityEngine;
using TMPro;
using System.Collections;

public class PopupUI : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;        // Texto que será digitado
    public TMP_Text instructionText;  // Texto fixo "Pressione E para avançar"

    private Coroutine typingCoroutine;
    private string fullText;
    private bool isTyping = false;

    void Start()
    {
        popupPanel.SetActive(false);
        instructionText.gameObject.SetActive(true); // Sempre ativo, só mostra o texto fixo
    }

    public void ShowPopup(string message)
    {
        popupPanel.SetActive(true);
        fullText = message;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    public void HidePopup()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        popupPanel.SetActive(false);
        isTyping = false;
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        popupText.text = "";
        foreach (char c in fullText)
        {
            popupText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
        typingCoroutine = null;
    }

    public void SkipTyping()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            popupText.text = fullText;
            isTyping = false;
        }
    }

    public bool IsTyping()
    {
        return isTyping;
    }
}
