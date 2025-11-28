using UnityEngine;

public class Throwable : MonoBehaviour
{
    // Opcional: permite configurar la masa del cubo desde el Inspector
    public float mass = 1f; 

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) 
            rb.mass = mass;
    }
}