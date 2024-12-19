using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerExitHive : MonoBehaviour
{
    public GameObject bee;
    private MoveBee mb;
    public GameObject pointSpawn;

    //------

    public int nbRoyalJelly;
    public int nbRoyalJellyTotal;

    //------


    public ManagerGame managerGame;

    public GameObject canvasPotOfHoney;
    public GameObject[] listeCanvaHoney;

    public ManagerBotBees managerBotBees;

    // Start is called before the first frame update
    void Start()
    {
        nbRoyalJelly = 0;
        listeCanvaHoney = new GameObject[canvasPotOfHoney.transform.childCount];
        for (int i = 0; i < listeCanvaHoney.Length; i++)
        {
            listeCanvaHoney[i] = canvasPotOfHoney.transform.GetChild(i).gameObject;
        }
        GenerateCanvaBee();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bee")
        {
            bee = other.gameObject;
            mb = bee.GetComponent<MoveBee>();
            if (mb.GetRoyalJelly())
            {

                mb.BeeStoped();
                mb.transform.position = pointSpawn.transform.position;
                nbRoyalJelly++;
                mb.SetRoyalJelly(false);
                CheckEndCondition();

                StartCoroutine(TimerCoroutine());
            }
        }else if (other.gameObject.tag == "BeeRobot")
        {
            managerBotBees.DeleteRobotBee(other.gameObject);
        }
    }
    IEnumerator TimerCoroutine()
    {
        // Attends 2 secondes
        yield return new WaitForSeconds(2f);
        // Action après 2 secondes
        mb.BeeFree();
    }
    private void CheckEndCondition()
    {
        if (nbRoyalJelly ==nbRoyalJellyTotal)
        {
            //managerGame.VictoireBee();
        }
    }

    private void GenerateCanvaBee()
    {

        for (int i = 0; listeCanvaHoney.Length > i; i++)
        {
            if (i == nbRoyalJelly)
            {
                listeCanvaHoney[i].SetActive(true);
            }
            else
            {
                listeCanvaHoney[i].SetActive(false);

            }
        }
    }
}
