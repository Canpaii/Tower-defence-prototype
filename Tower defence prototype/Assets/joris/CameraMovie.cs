using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovie : MonoBehaviour
{
    public Vector3 rotSpeed;
    public GameObject cam;
    public Vector3 moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.Rotate(rotSpeed * Time.deltaTime);
        cam.transform.Translate(moveSpeed * Time.deltaTime);
    }
}
