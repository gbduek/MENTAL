using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathingMinigame : MonoBehaviour
{
    public GameObject minigameUI;             // Painel do minigame
    public Image breathingCircle;             // C�rculo que expande/contrai
    public TMP_Text instructionText;          // Texto com instru��es

    public float phaseDuration = 4f;          // Dura��o de cada fase (inspirar/expirar)
    public float minSize = 1f;              // Tamanho m�nimo do c�rculo (escala normal)
    public float maxSize = 4f;              // Tamanho m�ximo do c�rculo (respira��o profunda)
    public float anxietyReductionPerSuccess = 15f;  // Quanto reduz a ansiedade em cada sucesso

    public CharacterMetrics characterMetrics; // Refer�ncia p�blica para arrastar o Player

    private bool isBreathing = false;
    private float phaseTimer = 0f;
    private bool isInhaling = true;            // true = expandindo, false = contraindo

    private Vector3 originalScale;
    private bool hasSucceededInPhase = false;

    void Start()
    {
        minigameUI.SetActive(false);
        originalScale = Vector3.one; // escala padr�o (1,1,1)

        if (characterMetrics == null)
        {
            Debug.LogWarning("CharacterMetrics n�o atribu�do no Inspector!");
        }
    }

    void Update()
    {
        if (!isBreathing) return;

        phaseTimer += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(phaseTimer / phaseDuration);

        // Usa SmoothStep para suavizar
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // Interpola o tamanho do c�rculo (expandir/contrair) suavemente
        float scale = Mathf.Lerp(
            isInhaling ? minSize : maxSize,
            isInhaling ? maxSize : minSize,
            smoothT
        );

        breathingCircle.rectTransform.localScale = new Vector3(scale, scale, 1f);

        // Atualiza o texto de instru��o
        instructionText.text = isInhaling ? "Inspire (segure ESPA�O)" : "Expire (solte ESPA�O)";

        // Verifica se o jogador fez a a��o certa para a fase
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

        // Quando a fase acabar, troca para a pr�xima
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
