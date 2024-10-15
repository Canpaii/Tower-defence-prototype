using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelPositionAndScaleToggle : MonoBehaviour
{
    public RectTransform panel;            // De RectTransform van het UI-paneel
    public Vector3 targetPosition;         // De doelpositie waar het paneel heen moet bewegen
    public Vector3 targetScale = Vector3.one;   // De doelschaal van het paneel
    public KeyCode toggleKey = KeyCode.Tab; // Toets om de actie te triggeren (aanpasbaar in de Inspector)
    public float transitionSpeed = 5f;     // Snelheid van de transitie van positie en schaal

    private Vector3 originalPosition;       // Oorspronkelijke positie van het paneel
    private Vector3 originalScale;          // Oorspronkelijke schaal van het paneel
    private bool isAtTargetPosition = false; // Houdt bij of het paneel op de doelpositie is
    private Coroutine moveCoroutine;        // Referentie naar de lopende coroutine voor de transitie

    void Start()
    {
        // Bewaar de oorspronkelijke positie en schaal van het paneel
        originalPosition = panel.anchoredPosition;
        originalScale = panel.localScale;
    }

    void Update()
    {
        // Luister naar de toets die de actie triggert
        if (Input.GetKeyDown(toggleKey))
        {
            TogglePanelPositionAndScale();
        }
    }

    void TogglePanelPositionAndScale()
    {
        // Stop de huidige transitie als die loopt
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // Controleer of het paneel naar de doelpositie of terug naar de originele positie moet gaan
        if (isAtTargetPosition)
        {
            // Beweeg terug naar de originele positie en schaal
            moveCoroutine = StartCoroutine(MovePanel(originalPosition, originalScale));
        }
        else
        {
            // Beweeg naar de doelpositie en doel schaal
            moveCoroutine = StartCoroutine(MovePanel(targetPosition, targetScale));
        }

        // Wissel de status
        isAtTargetPosition = !isAtTargetPosition;
    }

    IEnumerator MovePanel(Vector3 destinationPosition, Vector3 destinationScale)
    {
        // Terwijl de positie of schaal nog niet op de juiste plek is
        while (Vector3.Distance(panel.anchoredPosition, destinationPosition) > 0.1f ||
               Vector3.Distance(panel.localScale, destinationScale) > 0.01f)
        {
            // Beweeg de positie van het paneel richting de doelpositie met een bepaalde snelheid
            panel.anchoredPosition = Vector3.Lerp(panel.anchoredPosition, destinationPosition, Time.deltaTime * transitionSpeed);

            // Beweeg de schaal van het paneel richting de doelschaal met een bepaalde snelheid
            panel.localScale = Vector3.Lerp(panel.localScale, destinationScale, Time.deltaTime * transitionSpeed);

            // Wacht een frame voordat je verdergaat
            yield return null;
        }

        // Zorg ervoor dat de positie en schaal precies gelijk zijn aan de doelwaardes
        panel.anchoredPosition = destinationPosition;
        panel.localScale = destinationScale;
    }
}
