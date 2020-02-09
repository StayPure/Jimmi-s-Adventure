using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Overworld");
    }

    public void Quit()
    {
        Application.Quit();
    }
}