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

        if (index == disableClickIndex && Input.GetKeyDown(KeyCode.Tab))
        {
            AllowNextText();
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
}
