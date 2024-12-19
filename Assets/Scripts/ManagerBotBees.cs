using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ManagerBotBees : MonoBehaviour
{
    public GameObject BotBees;
    public List<GameObject> BotBeesList;
    public Transform pointSpawn;
    public GameObject point;
    public List<GameObject> pointDestinationRobotBee;
    private bool canCreateRobotBee;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GenerateRobotBee();
        }
        canCreateRobotBee = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(canCreateRobotBee)
        {
            StartCoroutine(TimerCoroutine());
            canCreateRobotBee = false;

        }
    }

    private Vector2 GetRandomPointOnCircle(float radius)
    {
        // Générer un angle aléatoire entre 0 et 2π (radians)
        float angle = Random.Range(0f, Mathf.PI * 2);

        // Calculer les coordonnées x et y
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return new Vector2(x, y);
    }

    private void GenerateRobotBee()
    {

        BotBeesList.Add(Instantiate(BotBees));
        BotBeesList[BotBeesList.Count - 1].transform.position = pointSpawn.transform.position;
        MoveRobotBee moveRobotBee = BotBeesList[BotBeesList.Count - 1].GetComponent<MoveRobotBee>();
        pointDestinationRobotBee.Add(Instantiate(point));

        Vector2 randomPoint = GetRandomPointOnCircle(27);
        pointDestinationRobotBee[BotBeesList.Count - 1].transform.position = new Vector3(randomPoint.x, Random.Range(20f, 50f), randomPoint.y);

        moveRobotBee.InstantiateRobotBee(pointSpawn.transform, pointDestinationRobotBee[BotBeesList.Count - 1].transform);
    }
    IEnumerator TimerCoroutine()
    {
        float nbSec = Random.Range(2f, 5f);
        // Attends 2 secondes
        yield return new WaitForSeconds(nbSec);
        GenerateRobotBee();
        canCreateRobotBee = true;
    }

    public void DeleteRobotBee(GameObject go)
    {
            MoveRobotBee moveRobotBee = go.GetComponent<MoveRobotBee>();
            if(!moveRobotBee.GetCommeIn() )
            {
                BotBeesList.Remove(go);
                Destroy(go);
            }
    }
}
