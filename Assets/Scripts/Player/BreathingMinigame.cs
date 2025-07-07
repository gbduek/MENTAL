using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathingMinigame : MonoBehaviour
{
    [Header("Minigame Elements")]
    public GameObject minigameUI;
    public RectTransform lungsImage;
    public Image respirationBarFill;
    public RectTransform targetMarker;
    public TMP_Text instructionText;
    public RectTransform respirationBarContainer;

    [Header("Settings")]
    public float fillDuration = 5f;  // Segundos para encher a barra toda
    public float successMargin = 0.4f;
    public Vector2 targetRange = new Vector2(0.5f, 0.9f);
    public float markerYOffset = -90f;

    [Header("Lung Scale")]
    public float minLungScale = 2f;
    public float maxLungScale = 2.2f;

    private float targetValue;
    private bool isHolding = false;
    private bool gameActive = false;
    private bool inputAllowed = false;


    private readonly string[] successMessages = new string[]
    {
        "Você conseguiu respirar.",
        "O ar entrou... está funcionando.",
        "Mais um passo pra retomar o controle.",
        "Está funcionando. Mantenha esse ritmo.",
        "Você venceu esse momento."
    };

    private readonly string[] failMessages = new string[]
    {
        "O ar não entrou... tenta de novo.",
        "Tá difícil, né? Respira mais uma vez.",
        "Seu corpo ainda tá lutando. Não desiste.",
        "Ainda não foi dessa vez. Respira outra vez.",
        "Calma... tenta de novo."
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenMinigame();
        }

        if (!gameActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isHolding = true;
            inputAllowed = true;
            instructionText.text = "Inspire... com calma.";
        }

        if (Input.GetKeyUp(KeyCode.Space) && inputAllowed)
        {
            CheckResult();
            ResetBar();
            SetNewTarget();
        }

        if (isHolding)
        {
            respirationBarFill.fillAmount += Time.unscaledDeltaTime / fillDuration;
            respirationBarFill.fillAmount = Mathf.Clamp01(respirationBarFill.fillAmount);

            float scale = Mathf.Lerp(minLungScale, maxLungScale, respirationBarFill.fillAmount);
            lungsImage.localScale = new Vector3(scale, scale, 1f);
        }
        else
        {
            lungsImage.localScale = Vector3.Lerp(
                lungsImage.localScale,
                Vector3.one * minLungScale,
                5f * Time.unscaledDeltaTime
            );
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMinigame();
        }
    }

    private void CheckResult()
    {
        float currentValue = respirationBarFill.fillAmount;
        bool success = Mathf.Abs(currentValue - targetValue) <= successMargin;

        if (success)
        {
            instructionText.text = successMessages[Random.Range(0, successMessages.Length)];
        }
        else
        {
            instructionText.text = failMessages[Random.Range(0, failMessages.Length)];
        }
    }

    private void ResetBar()
    {
        respirationBarFill.fillAmount = 0f;
        isHolding = false;
        inputAllowed = false;
    }

    private void SetNewTarget()
    {
        targetValue = Random.Range(targetRange.x, targetRange.y);

        float barWidth = respirationBarContainer.rect.width;
        float xPos = Mathf.Lerp(0, barWidth, targetValue);

        targetMarker.anchoredPosition = new Vector2(xPos, markerYOffset);
    }





    public void OpenMinigame()
    {
        Time.timeScale = 0f;
        minigameUI.SetActive(true);
        gameActive = true;
        ResetBar();
        SetNewTarget();
        instructionText.text = "Respire fundo... Segure ESPAÇO para inspirar.";

        Vector2 pos = targetMarker.anchoredPosition;
        targetMarker.anchoredPosition = new Vector2(pos.x, markerYOffset);
    }


    public void CloseMinigame()
    {
        Time.timeScale = 1f;
        gameActive = false;
        minigameUI.SetActive(false);
    }
}
