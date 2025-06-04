using UnityEngine;
using UnityEngine.UI;

public class ButtonMashQTE : MonoBehaviour
{
    public Slider qteBar;
    public float maxValue = 100f;
    public float fillPerClick = 10f;
    public float timeLimit = 5f;
    public float decayRate = 20f;
    [Range(0, 100)]
    public float initialValue = 0f;
    public KeyCode mashKey = KeyCode.E;

    private float currentValue = 0;
    private float timer;
    private bool isActive = false;

    public delegate void QTEResult(bool success);
    public event QTEResult OnQTEComplete;

    void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;

        if (currentValue >= maxValue)
        {
            CompleteQTE(true);
        }

        if (Input.GetKeyDown(mashKey))
        {
            currentValue += fillPerClick;
        }

        currentValue -= decayRate * Time.deltaTime;

        if(qteBar != null)
            qteBar.value = currentValue / maxValue;
        
        if (timer <= 0)
        {
            CompleteQTE(false);
        }
    }

    public void StartQTE()
    {
        currentValue = Mathf.Min(initialValue, maxValue);
        timer = timeLimit;
        isActive = true;
        if(qteBar != null)
            qteBar.gameObject.SetActive(true);
    }

    private void CompleteQTE(bool success)
    {
        isActive = false;
        if (qteBar != null)
            qteBar.gameObject.SetActive(false);
        OnQTEComplete?.Invoke(success);
    }
}

