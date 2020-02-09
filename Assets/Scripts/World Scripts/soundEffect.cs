using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(die());
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject); 
    }
}
