using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlyIn : MonoBehaviour
{
    public RectTransform panel;
    public Vector3 targetPosition; // De positie waar het paneel heen moet vliegen (on-screen)
    public Button triggerButton;
    public float flyInSpeed = 500f;
    public bool isPanelVisible = false;
    public KeyCode activatePanel;
    private Vector3 offScreenPosition; // De positie buiten het scherm (off-screen)
    private Coroutine movePanelCoroutine;

    void Start()
    {
        // Zet het paneel aan de zijkant buiten het scherm (bijvoorbeeld links buiten zicht)
        offScreenPosition = new Vector3(-Screen.width * 1.2f, targetPosition.y, 0f);
        panel.anchoredPosition = offScreenPosition;

        // Voeg een listener toe aan de knop
        triggerButton.onClick.AddListener(TogglePanel);
    }

    void Update()
    {
        // Luister naar de aangegeven toets (bijv. Escape)
        if (Input.GetKeyDown(activatePanel))
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
            // Beweeg het paneel terug naar links buiten het scherm (off-screen)
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
        }
        else
        {
            // Beweeg het paneel naar de doelpositie (on-screen)
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition));
        }

        isPanelVisible = !isPanelVisible;
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        while (Vector3.Distance(panel.anchoredPosition, destination) > 0.1f)
        {
            // Beweeg het paneel richting de doelpositie
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, destination, flyInSpeed * Time.deltaTime);
            yield return null;
        }

        // Zorg ervoor dat de positie precies gelijk is aan de doelpositie
        panel.anchoredPosition = destination;
        movePanelCoroutine = null;
    }
}
