using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVigil : MonoBehaviour
{

    public Vector2 turn;              // Pour la rotation via souris

    public float maxX;
    public float maxY;

    [SerializeField] LayerMask layerMask;

    void Start()
    {
    }
    void Update()
    {
        HandleRotation();
        HandleMouseClick();
    }

    private void HandleRotation()
    {
        // Rotation avec la souris
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");

        turn.y = Mathf.Clamp(turn.y, -maxY, maxY);
        turn.x = Mathf.Clamp(turn.x, -maxX, maxX);

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // Si clic gauche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Ray à partir de la position de la souris
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f)) // Longueur maximale du ray
            {
                Debug.Log($"Hit {hitInfo.collider.name} at {hitInfo.point}");

                // Faites que l'objet regarde le point cliqué
                Vector3 targetPosition = hitInfo.point;
                Vector3 direction = targetPosition - transform.position;
                direction.y = 0; // Optionnel : ignorez la hauteur si nécessaire
                transform.rotation = Quaternion.LookRotation(direction);
                Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 2f);

            }
            else
            {
                Debug.Log("Mouse click hit nothing");
            }
        }

    }
}
