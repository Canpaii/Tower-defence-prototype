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
        currentIndex = (currentIndex + 1) % prefabModels.Length;
        SpawnModel(currentIndex);
    }

    void PreviousModel()
    {
        currentIndex = (currentIndex - 1 + prefabModels.Length) % prefabModels.Length;
        SpawnModel(currentIndex);
    }

    void SpawnModel(int index)
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        currentModel = Instantiate(prefabModels[index], spawnPoint.position, spawnPoint.rotation);
    }
}