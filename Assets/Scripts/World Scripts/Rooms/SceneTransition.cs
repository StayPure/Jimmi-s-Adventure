using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPlacement;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float constantFadeWait;
    
    [Header("Interactable Transition Variables")]
    public bool inRange;
    public Signal interactableOn, interactableOff;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    private void Update()
    {
        if (inRange && Input.GetButtonDown("Interact"))
        {
            playerPlacement.runningValue = playerPosition;
            StartCoroutine(OpenCo());
            StartCoroutine(FadeCo()); 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !other.isTrigger)
        {
            inRange = true;
            if (!CompareTag("door"))
            {
                playerPlacement.runningValue = playerPosition;
                StartCoroutine(FadeCo());
            }
            else
            {
                interactableOn.Raise();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !other.isTrigger)
        {
            inRange = false;
            interactableOff.Raise();
        }
    }

    public IEnumerator OpenCo()
    {
        GetComponent<Animator>().SetBool("opened", true);
        yield return new WaitForSeconds(.5f);
    }
    
    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null)
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        
        yield return new WaitForSeconds(constantFadeWait);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while (!async.isDone)
            yield return null;
    }

    
}
