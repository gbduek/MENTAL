using UnityEngine;
using UnityEngine.UI;

public class CharacterMetrics : MonoBehaviour
{

    [Header("Anxiety")]
    [SerializeField] private float anxiety;
    [SerializeField] private float minAnxiety = 0.0f;
    [SerializeField] private float maxAnxiety = 100.0f;
    [SerializeField] private Slider anxietySlider;
    [SerializeField] private float anxietyDecayRate = -0.1f;
    //[SerializaField] private float anxietyDecayRate = 0.1f;
    //[SerializeField] private float anxietyDecayDelay = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anxiety = 0.0f;
        if(anxietySlider != null)
        {
            anxietySlider.maxValue = maxAnxiety;
            anxietySlider.minValue = minAnxiety;
            anxietySlider.value = anxiety;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Decay
        if (anxiety >= minAnxiety)
        {
            anxiety -= anxietyDecayRate * Time.deltaTime;
            anxiety = Mathf.Clamp(anxiety, minAnxiety, maxAnxiety);
        }
        handleAnxietySlider();
    }

    private void handleAnxietySlider()
    {
        if (anxietySlider != null)
        {
            anxietySlider.value = anxiety;
        }
    }

    public void setAnxiety(float value)
    {
        anxiety = Mathf.Clamp(value, minAnxiety, maxAnxiety);
    }

    public float getAnxiety()
    {
        Debug.Log("Anxiety called: " + anxiety);
        return anxiety;
    }

    public void addAnxiety(float value)
    {
        anxiety += value;
        anxiety = Mathf.Clamp(anxiety, minAnxiety, maxAnxiety);
    }
}
