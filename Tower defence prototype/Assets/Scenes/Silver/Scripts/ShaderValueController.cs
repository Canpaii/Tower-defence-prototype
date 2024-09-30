using System.Collections;
using UnityEngine;

public class ShaderValueController : MonoBehaviour
{
    public Material dissolveMaterial; // Het materiaal met de dissolve shader
    public Material newMaterial; // Het nieuwe materiaal dat je wilt toepassen
    public string dissolveProperty = "_DissolveAmount"; // De dissolve eigenschap in de shader
    public string timeProperty = "_FloatTime"; // De eigenschap in de shader voor de tijd
    public float duration = 3f; // De tijdsduur van de dissolve animatie
    public float timeSpeed = 3; // De snelheid waarmee _FloatTime omhoog gaat
    public MeshRenderer targetRenderer; // De renderer van het object

    private void Start()
    {
        // Start het dissolve proces wanneer het script begint
        StartCoroutine(DissolveEffect());
    }

    IEnumerator DissolveEffect()
    {
        float elapsedTime = 0f;
        float startValue = 1f; // Begin met een dissolve waarde van 1
        float endValue = 0f;   // Fade naar een dissolve waarde van 0
        float timeValue = 0f;  // Start de tijdswaarde op 0

        // Dissolve van 1 naar 0
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Interpoleer de dissolve waarde tussen start en eind
            dissolveMaterial.SetFloat(dissolveProperty, Mathf.Lerp(startValue, endValue, t));

            // Verhoog de tijdswaarde geleidelijk tijdens het dissolve proces
            timeValue += Time.deltaTime * timeSpeed;
            dissolveMaterial.SetFloat(timeProperty, timeValue);

            yield return null; // Wacht op de volgende frame
        }

        // Zorg ervoor dat de waarde exact op 0 wordt gezet aan het einde
        dissolveMaterial.SetFloat(dissolveProperty, endValue);

        // Wacht 0,5 seconden voordat je het materiaal vervangt
        yield return new WaitForSeconds(0.5f);

        // Vervang het materiaal van de renderer door het nieuwe materiaal
        targetRenderer.material = newMaterial;
    }
}