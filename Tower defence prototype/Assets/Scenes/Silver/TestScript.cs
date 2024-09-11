using UnityEngine;

public class TestScript : MonoBehaviour
{
    public ParticleSystem particleSystem;  // The particle system
    public Transform target;               // The target object
    public float particleSpeed = 10f;      // Speed of the particles
    public float detectionRadius = 10f;    // Detection radius (10 meters)

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        // Initialize the array for particles
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Update()
    {
        // Detect if any object with tag "FlyingUnit" is within range
        DetectFlyingUnits();

        // Get all active particles and set their velocity towards the target
        int numParticles = particleSystem.GetParticles(particles);
        for (int i = 0; i < numParticles; i++)
        {
            Vector3 direction = (target.position - particles[i].position).normalized;
            particles[i].velocity = direction * particleSpeed;
        }

        // Apply the updated particles back to the system
        particleSystem.SetParticles(particles, numParticles);
    }

    // Detect objects with the tag "FlyingUnit" within a 10-meter range
    void DetectFlyingUnits()
    {
        // Get all colliders within the detection radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Loop through the detected colliders
        foreach (Collider collider in hitColliders)
        {
            // Check if the collider has the tag "FlyingUnit"
            if (collider.CompareTag("FlyingUnit"))
            {
                // Emit particles if a FlyingUnit is within range
                EmitParticles(10);  // Emit 10 particles (customizable)
                break;  // Stop after detecting one flying unit
            }
        }
    }

    // Call this method to emit particles
    public void EmitParticles(int count)
    {
        particleSystem.Emit(count);
    }

    // Optional: Visualize the detection radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}