using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool opening = false;
    private Quaternion closedRot;
    private Quaternion openRot;

    void Start()
    {
        // Almacena la rotación inicial de la puerta (cerrada)
        closedRot = transform.rotation;
        // Calcula la rotación abierta (gira en el eje Y)
        openRot = closedRot * Quaternion.Euler(0, openAngle, 0); 
    }

    void Update()
    {
        if (opening)
        {
            // Interpola suavemente la rotación hacia el ángulo abierto
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        // Método llamado por el botón para iniciar la apertura
        opening = true;
    }
}