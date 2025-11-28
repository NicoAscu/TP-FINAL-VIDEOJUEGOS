using UnityEngine;

public class ButtonPress_Emissive : MonoBehaviour
{
    // Referencia al Renderer del objeto Puerta
    public Renderer doorRenderer; 
    
    // El material que queremos aplicar al ser presionado (debe tener la emisión activa)
    public Material activatedDoorMaterial; 
    
    // Opcional: Material para el botón al ser presionado
    public Material pressedButtonMaterial; 
    
    private bool activated = false;

    void OnCollisionEnter(Collision collision)
    {
        if (activated) return;

        // 1. Verificar colisión con el objeto lanzado
        if (collision.gameObject.CompareTag("ThrownObject"))
        {
            activated = true;
            Debug.Log("🎉 Botón Activado. Activando Emisión en la Puerta..."); 
            
            // 2. Cambiar material del botón (feedback)
            if (pressedButtonMaterial != null)
                GetComponent<Renderer>().material = pressedButtonMaterial;

            // 3. Iluminar la puerta cambiando su material
            if (doorRenderer != null && activatedDoorMaterial != null) 
            {
                // Asigna el nuevo material que tiene la propiedad Emission activada
                doorRenderer.material = activatedDoorMaterial; 
            }
            
            // Opcional: Detener el cubo después de golpear
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
    }
}