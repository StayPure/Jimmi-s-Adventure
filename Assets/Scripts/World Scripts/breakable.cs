using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : MonoBehaviour
{
    private Animator anime;
    public GameObject contains;

    void Start()
    {
        anime = GetComponent<Animator>();
    }
    
    public void smashed()
    {
        anime.SetBool("break", true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
        Instantiate(contains, transform.position, Quaternion.identity);
    }
}
