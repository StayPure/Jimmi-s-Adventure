using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Physics Variables")]
    public float speed;
    public Rigidbody2D ridge;
    
    [Header("Lifetime Variables")]
    public float lifetime;
    private float lifetimeSeconds;

    [Header("Mana Cost")]
    public float manaCost;

    private void Start()
    {
        lifetimeSeconds = lifetime;
    }

    private void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
            Destroy(gameObject);
    }

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        ridge.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {   
        if(CompareTag("Player"))
        {
            if (!other.CompareTag("Player") && !other.CompareTag("room")
                && !other.CompareTag("BossRoom")
                || (other.CompareTag("door") && !other.isTrigger))
                Destroy(gameObject);
        }
        else
        {
            if (!other.CompareTag("enemy") && !other.CompareTag("room") && !other.CompareTag("BossRoom")
                                           || (other.CompareTag("door") && !other.isTrigger))
                Destroy(gameObject);
        }
    }
}
