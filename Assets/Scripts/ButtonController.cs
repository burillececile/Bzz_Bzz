using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    //return to main menu
    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
