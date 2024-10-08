using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTyper : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] tutorialTexts;
    public bool[] disableClickForText;  // Bepaal voor elke index of klikken is uitgeschakeld
    public float typingSpeed = 0.05f;
    public Button continueButton;
    private int index = 0;
    private bool isTyping = false;
    private bool canClickToSkip = true;
    private Coroutine typingCoroutine;

    void Start()
    {
        continueButton.onClick.AddListener(AllowNextText);  // Voeg een listener toe voor de button
        StartTyping();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClickToSkip)
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (!disableClickForText[index]) // Als klikken toegestaan is
            {
                NextText();
            }
        }
    }

    void StartTyping()
    {
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        textDisplay.text = "";
        foreach (char letter in tutorialTexts[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;

        // Controleer of klikken is uitgeschakeld voor deze index
        if (disableClickForText[index])
        {
            DisableClickToSkip();
            continueButton.gameObject.SetActive(true);  // Toon de knop alleen als klikken is uitgeschakeld
        }
        else
        {
            canClickToSkip = true;  // Sta klikken toe als het niet is uitgeschakeld
        }
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textDisplay.text = tutorialTexts[index];  // Toon de volledige tekst
        isTyping = false;
    }

    public void NextText()
    {
        if (index < tutorialTexts.Length - 1)
        {
            index++;
            canClickToSkip = true;  // Reset klikken naar toegestaan voor de volgende tekst 
            StartTyping();
        }
        else
        {
            Debug.Log("Tutorial Finished");
        }
    }

    public void AllowNextText()
    {
        if (disableClickForText[index]) // Controleer of klikken is uitgeschakeld voor deze index
        {
            // Hier kun je extra logica toevoegen voor wat er moet gebeuren als de knop wordt ingedrukt
            // Bijvoorbeeld, als je een actie wilt uitvoeren die verdergaat met de tutorial
            NextText();  // Ga naar de volgende tekst en verberg de knop
        }
    }

    public void DisableClickToSkip()
    {
        canClickToSkip = false;  // Schakel klikken uit om tekst te skippen
    }
}
