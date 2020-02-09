using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverTyper : MonoBehaviour
{
    public float timeToWait;
    public float betweenLetters;

    public void Start()
    {
        StartCoroutine(TyperCo());
    }

    public IEnumerator TyperCo()
    {
        yield return new WaitForSeconds(timeToWait);
        GetComponent<TextMeshProUGUI>().text = "G";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GA";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAM";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAME";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAME O";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAME OV";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAME OVE";
        yield return new WaitForSeconds(betweenLetters);
        GetComponent<TextMeshProUGUI>().text = "GAME OVER";
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("StartMenu");
    }
}


