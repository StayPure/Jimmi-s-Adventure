using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : enemyAI
{
    [Header("Attacking Variables")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    
    [Header("Animator and Rigidbody")]
    public Rigidbody2D rigid;
    public Animator anime;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anime = GetComponent<Animator>();
        currentState = enemyState.idle;
        anime.SetBool("wakeUp", true);
    }
    
    void FixedUpdate()
    {
        checkDistance();
    }

    private void setAnimeFloat(Vector2 setVector)
    {
        anime.SetFloat("moveX", setVector.x);
        anime.SetFloat("moveY", setVector.y);
    } 
    
    protected void changeAnime(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                setAnimeFloat(Vector2.right);
            else if (direction.x < 0)
                setAnimeFloat(Vector2.left);
        }
        else if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
        {
            if (direction.y > 0)
                setAnimeFloat(Vector2.up);
            else if (direction.y < 0)
                setAnimeFloat(Vector2.down);
        }
    }
    
    protected virtual void checkDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
            && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                        target.position, 
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
            changeState(enemyState.walk);
            anime.SetBool("wakeUp", true);
        }
        else if (Vector2.Distance(homePosition, transform.position) < 0.001)
        {
            anime.SetBool("wakeUp", false);
        }
        else if (distance > chaseRadius)
        {
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                homePosition,
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
        }
        
    }

    protected void changeState(enemyState newState)
    {
        if (!newState.Equals(currentState))
            currentState = newState;
    }
}
