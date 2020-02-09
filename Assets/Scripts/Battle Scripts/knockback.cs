using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockback : MonoBehaviour
{
    public float thrust, knockTime, damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            enemyAI EnemyAI = other.GetComponent<enemyAI>();
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (enemy != null)
            {
                if (!CompareTag(other.tag))
                {
                    Vector2 difference = enemy.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                }
                
                if (other.gameObject.CompareTag("enemy") && other.isTrigger && !gameObject.CompareTag("enemy"))
                { 
                    EnemyAI.currentState = enemyState.stagger;
                    EnemyAI.enemyKnock(enemy, knockTime, damage);
                }
                else if (other.gameObject.CompareTag("Player") && other.isTrigger)
                {
                    if (player.currentState != PlayerState.stagger)
                    {
                        player.currentState = PlayerState.stagger;
                        player.playerKnock(knockTime, damage);
                    }
                }
            }
        }
        else if (other.gameObject.CompareTag("breakable") && gameObject.CompareTag("Player"))
        {
            other.GetComponent<breakable>().smashed();
        }
    }
}
