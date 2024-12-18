using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManagerGame : MonoBehaviour
{

    public GameObject Captcha;
    public ActionAlveolus actionAlveolus;
    public ManagerCaptcha mc;
    // Start is called before the first frame update
    void Start()
    {
        Captcha.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveCaptcha(GameObject go)
    {
        Debug.Log("rr : " +  go);
        actionAlveolus = go.GetComponent<ActionAlveolus>();
        Captcha.SetActive(true);
        mc = Captcha.GetComponent<ManagerCaptcha>();
    }

    public void DisableCaptcha(bool rep)
    {

        Debug.Log("BBBBBBBBB");
        mc.GenerateCaptcha();
        Captcha.SetActive(false);
        actionAlveolus.RapunzelCaptcha(rep);

    }

    public void ActiveEndGame(bool victoirBee)
    {
        SceneManager.LoadScene("Menu");
    }
}
