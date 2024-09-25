using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTyper : MonoBehaviour
{
    public TMP_Text uiText;               // De Text UI component
    public string[] textArray;         // De array met de tekst
    public float typingSpeed = 0.05f;  // De snelheid waarmee de tekst getypt wordt
    public Button nextButton;          // De knop om naar de volgende index te gaan
    public Button backButton;          // De knop om naar de vorige index te gaan

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);
        StartCoroutine(TypeText(textArray[currentIndex]));

        // Zorg ervoor dat de back button in het begin uitstaat, want we beginnen bij index 0
        UpdateButtonStates();
    }

    void OnNextButtonClicked()
    {
        if (!isTyping && currentIndex < textArray.Length - 1)
        {
            currentIndex++;
            StartCoroutine(TypeText(textArray[currentIndex]));
        }

        UpdateButtonStates();  // Always update button states to ensure correct button behavior
    }

    void OnBackButtonClicked()
    {
        if (!isTyping && currentIndex > 0)
        {
            currentIndex--;
            StartCoroutine(TypeText(textArray[currentIndex]));
        }

        UpdateButtonStates();  // Always update button states to ensure correct button behavior
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        uiText.text = "";  // Reset de tekst

        foreach (char letter in text.ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);  // Wacht een bepaald aantal seconden tussen letters
        }

        isTyping = false;
    }

    void UpdateButtonStates()
    {
        // Zorg ervoor dat de back button alleen werkt als je niet bij de eerste tekst bent
        backButton.interactable = currentIndex > 0;

        // Zorg ervoor dat de next button niet meer interactief is als je bij de laatste tekst bent
        if (currentIndex >= textArray.Length - 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
    }
}