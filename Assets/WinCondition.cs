using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class WinCondition : MonoBehaviour
{
    // El índice de la escena que creaste en Build Settings.
    // Si WinScene es la segunda en la lista, el índice es 1.
    public int winSceneIndex = 1; 

    // Se llama cuando otro Collider entra en el Trigger (la pared)
    void OnTriggerEnter(Collider other)
    {
        // 1. Verificar que el objeto que tocó es el jugador (o el objeto que tiene el Tag "Player")
        if (other.CompareTag("Player"))
        {
            // 2. Verificar la condición de victoria (si el botón fue presionado)
            if (GameManager.ButtonPressed)
            {
                Debug.Log("¡Condición de Victoria cumplida! Cargando escena...");
                
                // 3. Cargar la escena ganadora por índice.
                SceneManager.LoadScene(winSceneIndex);
            }
            else
            {
                Debug.Log("Has tocado la pared, pero el botón aún no ha sido presionado.");
            }
        }
    }
}