using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float force;
    public int damage;
    public Transform target;
    public float rotateSpeed;
    private float timer;
    public float homingDuration;
    private Rigidbody rb; 
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5);
    }
    private void LateUpdate()
    {   
        timer += Time.deltaTime;

        if(timer <= homingDuration)
        {
            Vector3 direction = target.position - rb.position;
            direction.Normalize();

            Vector3 amountToRotate = Vector3.Cross(direction, transform.forward) * Vector3.Angle(transform.forward, direction);

            rb.angularVelocity = -amountToRotate * rotateSpeed;

            rb.velocity = transform.forward * force;
        }  
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            print("Hit");
            
            Destroy(gameObject);
        }
    }
}
