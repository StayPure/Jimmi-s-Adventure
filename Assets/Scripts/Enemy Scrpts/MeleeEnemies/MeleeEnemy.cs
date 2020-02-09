using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : log
{
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anime = GetComponent<Animator>();
        currentState = enemyState.idle;
    }
    
    protected override void checkDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
                                    && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            anime.SetBool("walking", true);
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                target.position, 
                                moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
            changeState(enemyState.walk);
        }
        else if (distance <= chaseRadius && distance <= attackRadius
                                         && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle)))
        {
            StartCoroutine(attackCo());
        }
        else if (Vector2.Distance(homePosition, transform.position) < 0.001)
        {
            anime.SetBool("walking", false);
            anime.SetFloat("moveX", 0);
            anime.SetFloat("moveY", -1);
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

    public IEnumerator attackCo()
    {
        currentState = enemyState.attack;
        anime.SetBool("attack", true);
        yield return new WaitForSeconds(.33f);
        if (currentState != enemyState.special)
            currentState = enemyState.walk;
        anime.SetBool("attack", false);
    }
}
