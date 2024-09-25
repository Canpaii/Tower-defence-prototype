using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] prefabModels;  // Array van prefab-modellen
    public Button nextButton;          // Knop om naar het volgende model te gaan
    public Button prevButton;          // Knop om naar het vorige model te gaan
    public Transform spawnPoint;       // Plaats waar het model gespawned wordt

    private GameObject currentModel;   // Het huidige model in de scene
    private int currentIndex = 0;      // Huidige index van het model in de array

    private bool isDragging = false;   // Check of de gebruiker aan het slepen is
    private Vector3 previousMousePosition;
    public float rotationSpeed = 5f;   // Snelheid van rotatie tijdens slepen

    void Start()
    {
        nextButton.onClick.AddListener(NextModel);
        prevButton.onClick.AddListener(PreviousModel);
        SpawnModel(currentIndex);
        UpdateButtonStates();  // Zet knoppen in juiste staat bij opstarten
    }

    void Update()
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
        }
    }

    void NextModel()
    {
        if (currentIndex < prefabModels.Length - 1)
        {
            currentIndex++;
            SpawnModel(currentIndex);
            UpdateButtonStates();
        }
    }

    void PreviousModel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            SpawnModel(currentIndex);
            UpdateButtonStates();
        }
    }

    void SpawnModel(int index)
    {
        if (currentModel != null)
        {
            // Save the current rotation of the current model before destroying it
            spawnPoint.rotation = currentModel.transform.rotation;

            // Destroy the current model
            Destroy(currentModel);
        }

        // Spawn the new model and apply the spawnPoint rotation
        currentModel = Instantiate(prefabModels[index], spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateButtonStates()
    {
        // Disable de prevButton als je op het eerste model zit
        prevButton.interactable = currentIndex > 0;

        // Disable de nextButton als je op het laatste model zit
        nextButton.interactable = currentIndex < prefabModels.Length - 1;
    }
}