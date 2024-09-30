using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] prefabModels; 
    public Button nextButton;        
    public Button prevButton;          
    public Transform spawnPoint;      

    private GameObject currentModel;   
    private int currentIndex = 0;     

    private bool isDragging = false;   
    private Vector3 previousMousePosition;
    public float rotationSpeed = 5f;   

    void Start()
    {
        nextButton.onClick.AddListener(NextModel);
        prevButton.onClick.AddListener(PreviousModel);
        SpawnModel(currentIndex);
        UpdateButtonStates();  
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
            spawnPoint.rotation = currentModel.transform.rotation;
            Destroy(currentModel);
        }

        currentModel = Instantiate(prefabModels[index], spawnPoint.position, spawnPoint.rotation);
    }

    void UpdateButtonStates()
    {
        prevButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < prefabModels.Length - 1;
    }
}