using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcBullet : MonoBehaviour
{
    [SerializeField] private float force;
    public float lifeTime;
    public float damage;
    public Transform target;

    private float timer;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = target.position - rb.position;
        direction.Normalize();

        rb.velocity = direction * force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Hitable"))
        {
            other.gameObject.GetComponent<BuildingHp>().TakeDamage(damage);
            print("Hit");

            Destroy(gameObject);
        }
    }
}
