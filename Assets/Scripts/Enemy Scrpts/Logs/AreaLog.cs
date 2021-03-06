﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : log
{
    [Header ("Boundary for Attack")]
    public Collider2D boundary;

    protected override void checkDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= chaseRadius && distance > attackRadius
                                    && (currentState.Equals(enemyState.walk) || currentState.Equals(enemyState.idle))
                                    && boundary.bounds.Contains(target.transform.position))
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
            anime.SetFloat("moveX", 0); anime.SetFloat("moveY", -1);
            anime.SetBool("wakeUp", false);
        }
        else if (distance > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            Vector3 move = Vector3.MoveTowards(transform.position, 
                                                homePosition,
                                        moveSpeed * Time.deltaTime);
            changeAnime(move - transform.position);
            rigid.MovePosition(move);
        }
    }
}
