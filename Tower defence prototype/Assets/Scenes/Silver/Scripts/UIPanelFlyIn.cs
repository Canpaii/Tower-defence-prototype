using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelFlyIn : MonoBehaviour
{
    public RectTransform panel;         // De RectTransform van het UI-paneel dat moet bewegen
    public Vector3 targetPosition;      // De doelpositie waar het paneel heen moet
    public Button triggerButton;        // De knop die de animatie triggert
    public float flyInSpeed = 500f;     // Snelheid waarmee het panel naar de doelpositie beweegt
    public bool isPanelVisible = false; // Houdt bij of het paneel zichtbaar is of niet
    private Vector3 offScreenPosition;  // De startpositie van het paneel (onder het scherm)
    private Coroutine movePanelCoroutine; // Referentie naar de lopende coroutine

    void Start()
    {
        // Zet de beginpositie van het paneel onder het scherm (in het midden onderaan)
        offScreenPosition = new Vector3(targetPosition.x, -Screen.height, 0f);
        panel.anchoredPosition = offScreenPosition;

        // Voeg een listener toe voor de triggerButton
        triggerButton.onClick.AddListener(TogglePanel);
    }

    void Update()
    {
        // Luister naar de Escape toets
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        // Stop de huidige animatie als die loopt
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        if (isPanelVisible)
        {
            // Beweeg het paneel terug naar beneden
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
        }
        else
        {
            // Beweeg het paneel omhoog naar de doelpositie
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition));
        }

        isPanelVisible = !isPanelVisible;  // Wissel de zichtbaarheid status
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            // Beweeg de positie van het paneel richting de doelpositie met een bepaalde snelheid
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, destination, flyInSpeed * Time.deltaTime);
            yield return null;  // Wacht een frame voordat je verdergaat
        }

        // Zorg ervoor dat de positie precies gelijk is aan de doelpositie
        panel.anchoredPosition = destination;
        movePanelCoroutine = null;  // Reset de coroutine referentie wanneer klaar
    }
}