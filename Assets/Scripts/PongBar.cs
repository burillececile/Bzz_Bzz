using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PongBar : MonoBehaviour
{
    public Rigidbody rb;
    public int speed;

    public Vector3 direction = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        MoveLogic();
    }
    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector3>();
    }
    private void MoveLogic()
    {
        Vector3 newPosition = rb.position + direction * speed * Time.deltaTime;
        newPosition.x = Mathf.Round(newPosition.x * 1000f) / 1000f; // Arrondir à 3 décimales
        newPosition.y = 0f;
        newPosition.z = Mathf.Round(newPosition.z * 1000f) / 1000f;

        rb.MovePosition(newPosition);


    }
    public void UpdatePosition()
    {
        MoveLogic();
    }
}
