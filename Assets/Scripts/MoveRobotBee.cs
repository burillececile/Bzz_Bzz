using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MoveRobotBee : MonoBehaviour
{
    public Transform pointA; // Le premier point de déplacement
    public Transform pointB; // Le deuxième point de déplacement
    public float speed; // Vitesse de déplacement
    public float rotationSpeed ; // Vitesse de déplacement

    private float speedDef;
    private float rotationSpeedDef;

    private Vector3 targetPoint; // Le point actuel vers lequel se déplace l'objet

    private bool commeIn;
    void Start()
    {
        speedDef = speed;
        rotationSpeedDef = rotationSpeed;
        // Initialisation du point cible
    }

    void Update()
    {
        // Déplacement de l'objet vers le point cible
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        // Calculer la direction vers le point cible
        Vector3 directionToTarget = (targetPoint - transform.position).normalized;

        // Si la direction existe (non nulle), appliquer une rotation
        if (directionToTarget.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        // Si l'objet atteint le point cible, on inverse le point cible
        if (Vector3.Distance(transform.position, targetPoint) < 0.01f)
        {
            StartCoroutine(TimerCoroutine());
            //targetPoint = targetPoint == pointA.position ? pointB : pointA;
        }
        
    }



    IEnumerator TimerCoroutine()
    {
        float nbSec = Random.Range(2f, 5f);
        // Attends 2 secondes
        yield return new WaitForSeconds(nbSec);
        if(commeIn)
        {
            Transform point = pointA;
            pointA = pointB;
            pointB = point;
            targetPoint = pointB.position;

        }
        commeIn = false;
    }

    public void InstantiateRobotBee(Transform pointStart, Transform pointEnd)
    {
        pointA = pointStart;
        pointB = pointEnd;
        targetPoint = pointB.position;
        commeIn = true;
    }
    public bool GetCommeIn()
    {
        return commeIn;
    }
    public void BeeStoped()
    {
        speed = 0;
        rotationSpeed = 0;
    }

    public void BeeFree()
    {
        speed = speedDef;
        rotationSpeed = rotationSpeedDef;
    }
}
