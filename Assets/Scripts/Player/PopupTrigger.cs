using UnityEngine;

public class PopupTrigger : MonoBehaviour
{
    [TextArea]
    public string message;

    private PopupUI popupUI;
    private bool isPlayerInRange = false;
    private bool isPopupOpen = false;

    private void Start()
    {
        popupUI = FindFirstObjectByType<PopupUI>();


        if (popupUI == null)
        {
            Debug.LogError("PopupUI não encontrado na cena! Certifique-se que o objeto com PopupUI está ativo.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && isPopupOpen && popupUI != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (popupUI.IsTyping())
                {
                    popupUI.SkipTyping();
                }
                else
                {
                    popupUI.HidePopup();
                    isPopupOpen = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && popupUI != null)
        {
            isPlayerInRange = true;
            popupUI.ShowPopup(message);
            isPopupOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && popupUI != null)
        {
            isPlayerInRange = false;

            if (isPopupOpen)
            {
                popupUI.HidePopup();
                isPopupOpen = false;
            }
        }
    }
}
