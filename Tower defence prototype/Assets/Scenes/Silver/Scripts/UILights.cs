using UnityEngine;
using UnityEngine.UI;

public class LightIntensitySlider : MonoBehaviour
{
    public Light targetLight;   // Sleep hier je licht object in
    public Slider intensitySlider;  // Sleep hier de UI slider in

    void Start()
    {
        // Controleer of er een slider en light zijn toegewezen
        if (targetLight != null && intensitySlider != null)
        {
            // Koppel de slider waarde aan de intensiteit van het licht
            intensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
        }
    }

    // Functie die wordt aangeroepen wanneer de slider waarde verandert
    public void UpdateLightIntensity(float value)
    {
        targetLight.intensity = value;
    }
}