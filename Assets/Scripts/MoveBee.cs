using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBee : MonoBehaviour
{
    public Vector2 turn;              // Pour la rotation via souris
    public Rigidbody rb;              // Rigidbody attaché
    public int speed = 10;            // Vitesse de déplacement
    private Vector3 moveDirection;    // Direction actuelle du mouvement

    public Vector3 direction = Vector3.zero; // Pour le système d'Input

    public bool beeCanMove;
    public bool beeHaveRoyalJelly;

    void Start()
    {
        beeHaveRoyalJelly = false;
        beeCanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleRotation();
        if (beeCanMove)
        {

            MoveLogic();
        }
    }

    private void HandleRotation()
    {
        // Rotation souri
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");

        turn.y = Mathf.Clamp(turn.y, -50f, 50f);
        if (!beeCanMove)
        {

            turn.y = Mathf.Clamp(turn.y, -5f, 5f);
            turn.x = Mathf.Clamp(turn.x, -5f, 5f);
        }

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }

    private void MoveLogic()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = transform.forward * vertical + transform.right * horizontal;

        if (moveDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;     // stop mouvement
            rb.angularVelocity = Vector3.zero;  // stop rootation
        }
        else
        {
            rb.velocity = moveDirection.normalized * speed;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StopMovement();
    }

    private void OnTriggerExit(Collider other)
    {
        StopMovement();
        if (other.tag == "Hive")
        {
            transform.position = new Vector3(0, 0, 0);
            transform.rotation = Quaternion.identity;
            Debug.Log("On est sortie de la ruche");
        }
    }

    private void StopMovement()
    {
        // Stop mouvement Rigidbody
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Reset  position
        transform.position = new Vector3(
            Mathf.Round(transform.position.x * 100f) * 0.01f,
            Mathf.Round(transform.position.y * 100f) * 0.01f,
            Mathf.Round(transform.position.z * 100f) * 0.01f
        );
    }

    public void BeeStoped()
    {
        beeCanMove = false;
        rb.velocity = Vector3.zero;     // stop mouvement
        rb.angularVelocity = Vector3.zero;  // stop rootation
        StopMovement();
        Cursor.lockState = CursorLockMode.None;
    }
    public void BeeFree()
    {
        beeCanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetRoyalJelly(bool rep)
    {
        beeHaveRoyalJelly = rep;
    }
    public bool GetRoyalJelly()
    {
        return beeHaveRoyalJelly;
    }


}
