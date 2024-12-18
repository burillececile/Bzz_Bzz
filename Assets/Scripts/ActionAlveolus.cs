using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAlveolus : MonoBehaviour
{
    public GameObject bee;
    public GameObject pointSpawn;
    public ManagerGame managerGame;
    private MoveBee mb;
    public GameObject Alveolus;
    public SpriteRenderer spriteAlveolus;

    public Color colorStandar;
    public Color colorAlarme;

    public AudioSource audioAlarme;
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
            mb = bee.GetComponent<MoveBee>();
            if (!mb.GetRoyalJelly())
            {
                mb.BeeStoped();
                mb.transform.position = pointSpawn.transform.position;
                managerGame.ActiveCaptcha(gameObject);

            }
        }
    }

    public void RapunzelCaptcha(bool rep)
    {
        mb.BeeFree();
        if (rep)
        {

            Debug.Log("ALERTE");
            spriteAlveolus.color = colorAlarme;
            audioAlarme.Play();

            StartCoroutine(TimerCoroutine());
        }
        else
        {

            Debug.Log("Tout est ok ou donne la gelé");
            mb.SetRoyalJelly(true);
            Destroy(Alveolus);
        }
    }
    IEnumerator TimerCoroutine()
    {
        // Attends 2 secondes
        yield return new WaitForSeconds(5f);
        // Action après 2 secondes
        spriteAlveolus.color = colorStandar;
        audioAlarme.Stop();
    }
}
