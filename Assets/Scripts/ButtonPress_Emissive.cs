using UnityEngine;

public class ButtonPress_Emissive : MonoBehaviour
{
    // Objeto Renderer de la Puerta (para cambiar su material).
    public Renderer doorRenderer; 
    
    // Material de la Puerta con la propiedad Emission ACTIVA.
    public Material activatedDoorMaterial; 
    
    // Opcional: Material para el botón al ser presionado.
    public Material pressedButtonMaterial; 
    
    // Controla que el botón solo pueda ser activado una vez.
    private bool activated = false;

    void OnCollisionEnter(Collision collision)
    {
        if (activated) return;

        // Comprueba si el objeto que colisionó tiene el Tag "ThrownObject"
        if (collision.gameObject.CompareTag("ThrownObject"))
        {
            activated = true;
            Debug.Log("🎉 Botón Activado. Estado de victoria marcado y Puerta Iluminada."); 
            
            // 1. MARCA LA CONDICIÓN DE VICTORIA GLOBAL
            // El script WinCondition.cs verificará esta variable.
            GameManager.ButtonPressed = true;

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