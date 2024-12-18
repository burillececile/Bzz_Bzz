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

    // Start is called before the first frame update
    void Start()
    {
        nbRoyalJelly = 0;
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
        }
    }
    IEnumerator TimerCoroutine()
    {
        // Attends 2 secondes
        yield return new WaitForSeconds(2f);
        // Action après 2 secondes
        Debug.Log("2 secondes écoulées !");
        mb.BeeFree();
    }
    private void CheckEndCondition()
    {
        if (nbRoyalJelly ==nbRoyalJellyTotal)
        {
            managerGame.ActiveEndGame(true);
        }
    }
}
