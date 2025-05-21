using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathingMinigame : MonoBehaviour
{
    public GameObject minigameUI;             // Painel do minigame
    public Image breathingCircle;             // Círculo que expande/contrai
    public TMP_Text instructionText;          // Texto com instruções

    public float phaseDuration = 4f;          // Duração de cada fase (inspirar/expirar)
    public float minSize = 1f;              // Tamanho mínimo do círculo (escala normal)
    public float maxSize = 4f;              // Tamanho máximo do círculo (respiração profunda)
    public float anxietyReductionPerSuccess = 15f;  // Quanto reduz a ansiedade em cada sucesso

    public CharacterMetrics characterMetrics; // Referência pública para arrastar o Player

    private bool isBreathing = false;
    private float phaseTimer = 0f;
    private bool isInhaling = true;            // true = expandindo, false = contraindo

    private Vector3 originalScale;
    private bool hasSucceededInPhase = false;

    void Start()
    {
        minigameUI.SetActive(false);
        originalScale = Vector3.one; // escala padrão (1,1,1)

        if (characterMetrics == null)
        {
            Debug.LogWarning("CharacterMetrics não atribuído no Inspector!");
        }
    }

    void Update()
    {
        if (!isBreathing) return;

        phaseTimer += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(phaseTimer / phaseDuration);

        // Usa SmoothStep para suavizar
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // Interpola o tamanho do círculo (expandir/contrair) suavemente
        float scale = Mathf.Lerp(
            isInhaling ? minSize : maxSize,
            isInhaling ? maxSize : minSize,
            smoothT
        );

        breathingCircle.rectTransform.localScale = new Vector3(scale, scale, 1f);

        // Atualiza o texto de instrução
        instructionText.text = isInhaling ? "Inspire (segure ESPAÇO)" : "Expire (solte ESPAÇO)";

        // Verifica se o jogador fez a ação certa para a fase
        if (!hasSucceededInPhase)
        {
            if (isInhaling && Input.GetKey(KeyCode.Space))
            {
                hasSucceededInPhase = true;
                characterMetrics.addAnxiety(-anxietyReductionPerSuccess);
            }
            else if (!isInhaling && !Input.GetKey(KeyCode.Space))
            {
                hasSucceededInPhase = true;
                characterMetrics.addAnxiety(-anxietyReductionPerSuccess);
            }
        }

        // Quando a fase acabar, troca para a próxima
        if (phaseTimer >= phaseDuration)
        {
            phaseTimer = 0f;
            isInhaling = !isInhaling;
            hasSucceededInPhase = false;
        }

        // Fecha o minigame se apertar ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMinigame();
        }
    }

    public void OpenMinigame()
    {
        Time.timeScale = 0f;  // pausa o jogo principal
        isBreathing = true;
        minigameUI.SetActive(true);

        phaseTimer = 0f;
        isInhaling = true;
        hasSucceededInPhase = false;
    }

    public void CloseMinigame()
    {
        isBreathing = false;
        Time.timeScale = 1f;  // retoma o jogo
        minigameUI.SetActive(false);
    }
}
