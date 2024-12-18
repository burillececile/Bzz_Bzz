using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerExitHive : MonoBehaviour
{
    public GameObject bee;
    private MoveVigil mv;
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
            mv = bee.GetComponent<MoveVigil>();
            mv.BeeStoped();
            mv.transform.position = pointSpawn.transform.position;
            nbRoyalJelly++;
            mv.SetRoyalJelly(false);
            CheckEndCondition();

            mv.beeCanMove = true;
        }
    }

    private void CheckEndCondition()
    {
        if (nbRoyalJelly ==nbRoyalJellyTotal)
        {
            managerGame.ActiveEndGame(true);
        }
    }
}
