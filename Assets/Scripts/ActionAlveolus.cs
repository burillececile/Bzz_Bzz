using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAlveolus : MonoBehaviour
{
    public GameObject bee;
    public GameObject pointSpawn;
    public ManagerGame managerGame;
    private MoveVigil mv;
    // Start is called before the first frame update
    void Start()
    {
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
            managerGame.ActiveCaptcha(gameObject);
        }
    }

    public void RapunzelCaptcha(bool rep)
    {
        mv.BeeFree();
        if (rep)
        {

            Debug.Log("ALERTE");
        }
        else
        {

            Debug.Log("Tout est ok ou donne la gelé");
            mv.SetRoyalJelly(true);
        }
    }
}
