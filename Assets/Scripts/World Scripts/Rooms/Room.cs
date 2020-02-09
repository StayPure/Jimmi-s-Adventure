using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("In Room")]
    public enemyAI[] enemies;
    public breakable[] breakables;
    
    [Header("Camera and Boss Status")]
    public GameObject virtualCamera;
    public BoolValue bossDefeated;
    public bool areaAnnoucemnt;
    
    [Header("Music")]
    public AudioClip roomMusic;
    public AudioSource speaker;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (bossDefeated != null)
            {
                if (!bossDefeated.RuntimeValue)
                {
                    //Activate all enemies and breakables
                    foreach (enemyAI e in enemies)
                        ChangeActivation(e, true);

                    foreach (breakable p in breakables)
                        ChangeActivation(p, true); 
                }
            }
            else
            {
                //Activate all enemies and breakables
                foreach (enemyAI e in enemies)
                    ChangeActivation(e, true);

                foreach (breakable p in breakables)
                    ChangeActivation(p, true);
            }

            if (roomMusic != null && roomMusic != speaker.clip)
            {
                speaker.clip = roomMusic;
                speaker.Play();
            }

            if (areaAnnoucemnt)
                StartCoroutine(areaChange());

            virtualCamera.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //Deactivate all enemies and breakables
            foreach (enemyAI e in enemies)
                ChangeActivation(e, false);

            foreach (breakable p in breakables)
                ChangeActivation(p, false);
            
            virtualCamera.SetActive(false);
        }
    }

    private IEnumerator areaChange()
    {
        GameObject areaName = GameObject.FindWithTag("Canvas").transform.GetChild(0).gameObject;
        areaName.GetComponent<Text>().text = gameObject.name;
        areaName.GetComponent<Animator>().SetBool("areaChange", true);
        yield return new WaitForSeconds(1);
        areaName.GetComponent<Animator>().SetBool("areaChange", false);
    }

    public void ChangeActivation(Component component, bool state)
    {
        component.gameObject.SetActive(state);
    }
}
