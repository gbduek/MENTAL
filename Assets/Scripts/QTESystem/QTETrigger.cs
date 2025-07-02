using UnityEngine;
using TMPro;

public class QTETrigger : MonoBehaviour
{
    public ButtonMashQTE qte;
    public TMP_Text succesText;
    public TMP_Text instrucitonsText;

    public void StartQTE()
    {
        if(instrucitonsText != null)
        {
            instrucitonsText.gameObject.SetActive(true);
        }
        if (succesText != null)
            succesText.gameObject.SetActive(false);
        qte.OnQTEComplete += HandleQTEResult;
        qte.StartQTE();
    }

    void HandleQTEResult(bool success)
    {
        if (success)
            Debug.Log("Mash QTE success!");
        else
            Debug.Log("Mash QTE failed!");
        if (instrucitonsText != null)
        {
            instrucitonsText.gameObject.SetActive(false);
        }
        if (succesText != null)
        {
            succesText.gameObject.SetActive(true);
            succesText.text = success ? "Success!" : "Failed!";
        }
    }
}
