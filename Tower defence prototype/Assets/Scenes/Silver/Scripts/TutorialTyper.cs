using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTyper : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] tutorialTexts;
    public int disableClickIndex = 5;  // The index where clicking is disabled
    public float typingSpeed = 0.05f;
    public Button continueButton;
    public int continuePlacedTurretIndex = 7;  // The index where placedFirstTurret needs to be checked
    public bool continuePlacedTurret = false;  // The condition that controls whether the user can continue
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
        if (Input.GetMouseButtonDown(0) && canClickToSkip)
        {
            if (isTyping)
            {
                SkipTyping();
            }
            else if (CanProceed())  // Ensure CanProceed is true
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

        // Show/hide the continue button based on the current index
        if (index == disableClickIndex)
        {
            DisableClickToSkip();
            continueButton.gameObject.SetActive(true);  // Show button when clicking is blocked
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
        textDisplay.text = tutorialTexts[index];  // Display full text
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
        // Allow progression only if at disableClickIndex and CanProceed is true
        if (index == disableClickIndex)
        {
            NextText();  // Progress to the next text when the button is clicked
        }
        else if (CanProceed())  // Check if we can proceed normally
        {
            NextText();
        }
    }

    public void DisableClickToSkip()
    {
        canClickToSkip = false;
    }

    // Function to check if conditions are met to proceed
    private bool CanProceed()
    {
        // Check if the index is where turret placement is required
        if (index == continuePlacedTurretIndex && !continuePlacedTurret)
        {
            return false;  // Don't allow progression until continuePlacedTurret is true
        }

        // Block progression at disableClickIndex unless continue button is clicked
        if (index == disableClickIndex)
        {
            return false;  // Block clicking to proceed at this specific index
        }

        return true;  // Allow progression otherwise
    }
}
