using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speed;
    public Vector2 directionToMove;
    public Rigidbody2D myRigidbody2D;
    
    [Header("Lifetime Variables")]
    public float lifetime;
    private float lifetimeSeconds;
    

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    private void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
            Destroy(gameObject);
    }

    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody2D.velocity = initialVelocity * speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("room")) 
            Destroy(gameObject);
    }
}
