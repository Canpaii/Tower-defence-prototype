using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public GameObject levelUpUI;
    public bool placed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EnableCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
