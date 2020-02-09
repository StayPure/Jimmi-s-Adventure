using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Sprites")]
    public Image[] hearts;
    public Sprite full, half, empty;

    [Header("Actual Heart Values")]
    public FloatValue containers;
    public FloatValue playerCurrentHealth;

    void Start()
    {
       updateHearts();
    }

    public void initHearts()
    {
        for (int i = 0; i < containers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = full;
        }
    }

    public void updateHearts()
    {
        initHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        
        for (int i = 0; i < containers.RuntimeValue; i++)
        {
            if (i <= tempHealth - 1)
                hearts[i].sprite = full;
            else if (i >= tempHealth)
                hearts[i].sprite = empty;
            else
                hearts[i].sprite = half;
        }
    }
}
