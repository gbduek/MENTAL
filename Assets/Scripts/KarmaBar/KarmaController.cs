using UnityEngine;

public class KarmaController : MonoBehaviour
{
    public GameObject karmaSlider;
    static float karma = 3.0f;
    static float karmaMax = 6.0f;
    static float karmaMin = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        karmaSlider.GetComponent<UnityEngine.UI.Slider>().maxValue = karmaMax;
    }

    public float getKarma()
    {
        // This function will be used to get the current karma value
        return karma;
    }

    public void setKarma(float newKarma)
    {
        // This function will be used to set the current karma value
        if (newKarma > karmaMax)
        {
            karma = karmaMax;
        }
        else if (newKarma < karmaMin)
        {
            karma = karmaMin;
        }
        else
        {
            karma += newKarma;
        }
    }

    // Update is called once per frame
    void Update()
    {
        karmaSlider.GetComponent<UnityEngine.UI.Slider>().value = karma;
    }
}
