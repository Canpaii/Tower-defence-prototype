using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] prefabModels;
    public TMP_Text primaryTextUI;
    public TMP_Text secondaryTextUI;
    public string[] textArray;
    public string[] secondTextArray;
    public float typingSpeed = 0.05f;
    public Button nextButton;
    public Button backButton;
    public Transform spawnPoint;
    public float rotationSpeed = 5f;
    public float decelerationSpeed = 2f;

    private GameObject currentModel;
    private int currentIndex = 0;
    private bool isDragging = false;
    private bool isTyping = false;
    private Vector3 previousMousePosition;
    private float lastRotationSpeed = 0f;
    private Coroutine typingCoroutine;

    void Start()
    {
        nextButton.onClick.AddListener(OnNextClicked);
        backButton.onClick.AddListener(OnBackClicked);

        SpawnModel(currentIndex);
        typingCoroutine = StartCoroutine(TypeText(textArray[currentIndex], secondTextArray[currentIndex]));

        UpdateButtonStates();
    }

    void Update()
    {
        HandleModelRotation();
    }

    void HandleModelRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && currentModel != null)
        {
            Vector3 deltaMouse = Input.mousePosition - previousMousePosition;
            float rotationY = deltaMouse.x * rotationSpeed * Time.deltaTime;
            currentModel.transform.Rotate(Vector3.up, -rotationY, Space.World);
            previousMousePosition = Input.mousePosition;

            lastRotationSpeed = rotationY;
        }
        else if (currentModel != null && Mathf.Abs(lastRotationSpeed) > 0.01f)
        {
            currentModel.transform.Rotate(Vector3.up, -lastRotationSpeed, Space.World);
            lastRotationSpeed = Mathf.Lerp(lastRotationSpeed, 0, Time.deltaTime * decelerationSpeed);
        }
    }

    void OnNextClicked()
    {
        if (!isTyping && currentIndex < prefabModels.Length - 1 && currentIndex < textArray.Length - 1)
        {
            currentIndex++;
            SpawnModel(currentIndex);
            typingCoroutine = StartCoroutine(TypeText(textArray[currentIndex], secondTextArray[currentIndex]));
            UpdateButtonStates();
        }
        else if (isTyping)
        {
            // Als er wordt geklikt tijdens het typen, stop de Coroutine en toon direct de volledige tekst
            SkipTyping();
        }
    }

    void OnBackClicked()
    {
        if (!isTyping && currentIndex > 0)
        {
            currentIndex--;
            SpawnModel(currentIndex);
            typingCoroutine = StartCoroutine(TypeText(textArray[currentIndex], secondTextArray[currentIndex]));
            UpdateButtonStates();
        }
        else if (isTyping)
        {
            // Als er wordt geklikt tijdens het typen, stop de Coroutine en toon direct de volledige tekst
            SkipTyping();
        }
    }

    IEnumerator TypeText(string primaryText, string secondaryText)
    {
        isTyping = true;
        primaryTextUI.text = "";
        secondaryTextUI.text = "";

        // Typ de primary text
        foreach (char letter in primaryText.ToCharArray())
        {
            primaryTextUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Typ de secondary text
        foreach (char letter in secondaryText.ToCharArray())
        {
            secondaryTextUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // Stop het typen en toon de volledige tekst direct
    void SkipTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Toon direct de volledige tekst
        primaryTextUI.text = textArray[currentIndex];
        secondaryTextUI.text = secondTextArray[currentIndex];

        isTyping = false;
    }

    void SpawnModel(int index)
    {
        if (currentModel != null)
        {
            spawnPoint.rotation = currentModel.transform.rotation;
            Destroy(currentModel);
        }

        currentModel = Instantiate(prefabModels[index], spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateButtonStates()
    {
        backButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < prefabModels.Length - 1 && currentIndex < textArray.Length - 1;
    }
}
