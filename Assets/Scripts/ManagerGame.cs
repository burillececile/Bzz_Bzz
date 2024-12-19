using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerGame : MonoBehaviour
{

    public GameObject CaptchaBee;
    public GameObject CaptchaVigil;
    public ActionAlveolus actionAlveolus;
    public ManagerCaptcha mcBee;
    public ManagerCaptcha mcVigil;
    public MoveVigil moveVigil;
    public float TimeGameMinute;
    // Start is called before the first frame update
    void Start()
    {
        CaptchaBee.SetActive(false);
        CaptchaVigil.SetActive(false);
        StartCoroutine(TimerCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveCaptcha(GameObject go)
    {
        actionAlveolus = go.GetComponent<ActionAlveolus>();
        CaptchaBee.SetActive(true);
    }

    public void DisableCaptcha(bool rep)
    {

        mcBee.GenerateCaptcha();
        CaptchaBee.SetActive(false);
        actionAlveolus.RapunzelCaptcha(rep);

    }
    public void ActiveCaptchaVigil()
    {
        CaptchaVigil.SetActive(true);
    }

    public void DisableCaptchaVigil(bool rep)
    {

        mcVigil.GenerateCaptcha();
        CaptchaVigil.SetActive(false);
        moveVigil.RapunzelCaptcha(rep);

    }


    IEnumerator TimerCoroutine()
    {
        // Attends 2 secondes
        yield return new WaitForSeconds(TimeGameMinute*60f);
        // Action après 2 secondes
        if (moveVigil.BeeIsCaptured())
        {
            //Victoire vigile
            VictoireVigil();
        }
        else
        {
            // Victoire Bee
            VictoireBee();
        }
    }

    public void VictoireBee()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Bee gagne");
        // Scene Bee
    }
    public void VictoireVigil()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Vigil gagne");
        // Scene Bee
    }
}
