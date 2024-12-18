using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    //Play Game Button
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    //Multi Player mode button
    public void MultiPlayer()
    {
        Debug.Log("Bzzzz");
    }
    //Quit Game button
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }


}