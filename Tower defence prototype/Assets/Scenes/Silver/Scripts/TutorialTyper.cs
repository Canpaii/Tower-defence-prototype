using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTyper : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] tutorialTexts;
    public int disableClickIndex = 5;
    public float typingSpeed = 0.05f;
    public Button continueButton;
    public RectTransform alles;
    public int continuePlacedTurretIndex = 7;
    public bool continuePlacedTurret = false;
    private int index = 0;
    private bool isTyping = false;
    private bool canClickToSkip = true;
    private Coroutine typingCoroutine;

    void Start()
    {
        continueButton.onClick.AddListener(AllowNextText);
        StartTyping();
    }

    void Update()
    {
        // Check for mouse button click to skip typing or proceed to the next text
        if (Input.GetMouseButtonDown(0) && canClickToSkip)
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (CanProceed())
            {
                NextText();
            }
        }

        // Allow proceeding to next text with Tab key
        if (index == disableClickIndex && Input.GetKeyDown(KeyCode.Tab))
        {
            AllowNextText();
        }

        // End the tutorial and hide UI on pressing the "8" key
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EndTutorial();
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

        if (index == disableClickIndex)
        {
            DisableClickToSkip();
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            canClickToSkip = true;
        }
    }

    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        textDisplay.text = tutorialTexts[index];
        isTyping = false;
    }

    public void NextText()
    {
        if (index < tutorialTexts.Length - 1)
        {
            index++;
            canClickToSkip = true;
            StartTyping();
        }
        else
        {
            Debug.Log("Tutorial Finished");
        }
    }

    public void AllowNextText()
    {
        if (index == disableClickIndex)
        {
            NextText();
        }
        else if (CanProceed())
        {
            NextText();
        }
    }

    public void DisableClickToSkip()
    {
        canClickToSkip = false;
    }

    private bool CanProceed()
    {
        if (index == continuePlacedTurretIndex && !continuePlacedTurret)
        {
            return false;
        }

        if (index == disableClickIndex)
        {
            return false;
        }

        return true;
    }

    // Method to end the tutorial
    public void EndTutorial()
    {
        // Optionally, you can hide or disable the UI elements here
        textDisplay.gameObject.SetActive(false); // Hide the text display
        continueButton.gameObject.SetActive(false); // Hide the continue button
        alles.gameObject.SetActive(false);
        Debug.Log("Tutorial Ended");
        // You can add additional logic here to handle what happens after ending the tutorial
    }
}
