using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactableCue : MonoBehaviour
{
    public GameObject InteractableCue;

    public void enable()
    {
        InteractableCue.SetActive(true);
    }

    public void disable()
    {
        InteractableCue.SetActive(false);
    }
    
}
