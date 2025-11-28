using UnityEngine;

public class PickUpRaycast : MonoBehaviour
{
    public float rayDistance = 3f; // Distancia máxima para raycasting
    public LayerMask grabbableLayer; // Filtro por la capa Grabbable
    public GameObject uiPressE; // Referencia al texto UI
    public Transform holdPoint; // Punto donde se sostiene el objeto
    public float throwForce = 15f; // Fuerza de lanzamiento
    
    private GameObject heldObject;

    void Update()
    {
        if (heldObject == null)
            HandleRaycast();
        else
            HandleThrow();
    }

    void HandleRaycast()
    {
        // Raycast desde el centro de la cámara
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Dibuja el Raycast en la escena para debug (solo visible en el Scene View)
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow); 

        if (Physics.Raycast(ray, out hit, rayDistance, grabbableLayer))
        {
            // Si el rayo impacta un objeto en la capa 'Grabbable'
            if (uiPressE != null) uiPressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp(hit.collider.gameObject);
            }
        }
        else
        {
            // Si no mira un objeto 'Grabbable'
            if (uiPressE != null) uiPressE.SetActive(false);
        }
    }

    void PickUp(GameObject obj)
    {
        heldObject = obj;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        
        // Desactiva la física y lo mueve al HoldPoint
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Se convierte en hijo del HoldPoint
        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;
        // Asegura que no tenga tag de lanzado cuando es recogido
        heldObject.tag = "Untagged"; 
    }

    void HandleThrow()
    {
        // Click izquierdo o Space para lanzar
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            ThrowHeld();
        } 
        // Soltar con Q
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DropHeld();
        }
    }

    void ThrowHeld()
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        
        // Desvincula del HoldPoint
        heldObject.transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            // Usa la dirección de la cámara para lanzar
            Vector3 throwDir = Camera.main.transform.forward; 
            // Aplica la fuerza de lanzamiento
            rb.AddForce(throwDir * throwForce, ForceMode.Impulse);
        }
        
        // Asigna el Tag para que el botón lo detecte
        heldObject.tag = "ThrownObject";
        heldObject = null;
    }

    void DropHeld()
    {
        if (heldObject == null) return;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        heldObject.transform.SetParent(null);
        if (rb != null) rb.isKinematic = false;
        heldObject = null;
    }
}